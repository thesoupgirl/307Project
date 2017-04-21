using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roost;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Roost.Interfaces;
using Roost.Models;
using System.IO;
using System.Text;

namespace Roost.Controllers
{
    [Route("api/activities")]
    public class ActivitiesController : Controller
    {

        static DBHelper db = new DBHelper();

        // Initialize the tables as Table objects
        Table activitiesTable = Table.LoadTable(db.client, "RoostActivities");
        Table chatTable = Table.LoadTable(db.client, "RoostChats");

        // GET: /api/activities/{userId}/{dist}
        // Gets list of activities within certain radius of user based on popularity
        [HttpGet("{userId}/{dist}/search")]
        public async Task<string> Search(string userId, string dist)
        {
            try
            {
                ScanFilter scanFilter = new ScanFilter();
                scanFilter.AddCondition("status", ScanOperator.Equal, "open");

                // Start the search
                Search search = activitiesTable.Scan(scanFilter);

                string data = "{ \"data\": [ ";
                int baseLen = data.Length;

                // Put all results into a list.
                List<Document> docList = new List<Document>();

                do
                {
                    docList = await search.GetNextSetAsync();

                    foreach (Document d in docList)
                    {
                        // Only return activities the user is not in.
                        List<string> members = d["members"].AsListOfString();

                        if (!members.Contains(userId))
                            data = data + d.ToJson().ToString() + " , ";

                    }
                } while (!search.IsDone);

                // Clean up the string so the frontend can parse it.
                if (data.Length > baseLen)
                    data = data.Remove(data.Length - 3);

                data = data + " ] }";
                data = data.Replace("\\", "");
                Console.WriteLine("Second time: " + data);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET /api/activities/{userId}/getactivities
        // Get all the activities a user is in.
        [HttpGet("{userId}/getactivities")]
        public async Task<string> GetActivities(string userId)
        {
            try
            {
                ScanFilter scanFilter = new ScanFilter();

                // Start the search
                Search search = activitiesTable.Scan(scanFilter);

                string data = "{ \"data\": [ ";
                int baseLen = data.Length;

                // Put all results into a list.
                List<Document> docList = new List<Document>();

                do
                {
                    docList = await search.GetNextSetAsync();

                    foreach (Document d in docList)
                    {   // Find all the groups a user belongs to and return them as JSON.
                        List<string> members = d["members"].AsListOfString();

                        if (members.Contains(userId))
                            data = data + d.ToJson().ToString() + " , ";
                    }
                } while (!search.IsDone);

                // Clean up the string so the frontend can parse it.
                if (data.Length > baseLen)
                    data = data.Remove(data.Length - 3);

                data = data + " ] }";
                data = data.Replace("\\", "");
                Console.WriteLine("Second time: " + data);
                return data;

            }
            catch (Exception)
            {
                return null;
            }
        }

        // POST: /api/activities/{userId}/createactivity
        // Creates an activity
        [HttpPost("{userId}/createactivity")]
        public async Task<HttpResponseMessage> CreateActivity(string userId)
        {
            // Use a randomly generated number for activity and chat IDs
            Random r = new Random();

            // Convert them to strings because that's how they're stored
            string activityID = r.Next(1000000).ToString();
            string chatID = r.Next(1000000).ToString();

            try
            {
                // Take the uploaded avatar and convert it to a base64 string.
                // This is how the images will be stored in the table.
                string base64Image = null;
                if (!String.IsNullOrEmpty(Request.Form["avatar"]))
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(Request.Form["avatar"]);
                    base64Image = Convert.ToBase64String(imageArray);
                }
                else
                {
                    base64Image = "none";
                }

                List<string> members = new List<string> { userId };

                Console.WriteLine(Request.Form["name"]);
                Console.WriteLine(Request.Form["description"]);
                Console.WriteLine(Request.Form["category"]);
                Console.WriteLine(Request.Form["latitude"]);
                Console.WriteLine(Request.Form["longitude"]);
                Console.WriteLine(Request.Form["status"]);
                Console.WriteLine(Request.Form["maxSize"]);

                // Don't let them create the activity just for themselves.
                if (Request.Form["maxSize"] == 1)
                {
                    Response.StatusCode = 400;
                    HttpResponseMessage respons = new HttpResponseMessage();
                    return respons;
                }

                // Add the activity to the database table
                await db.client.PutItemAsync(
                    tableName: "RoostActivities",
                    item: new Dictionary<String, AttributeValue>
                    {
                        // Primary key: The unique activity id
                        {"ActivityId", new AttributeValue { S = activityID } },

                        // The number of people in the group
                        {"numMembers", new AttributeValue { N = members.Count().ToString() } },

                        // The name of the group
                        {"name", new AttributeValue { S = Request.Form["name"] } },

                        // The description of the group
                        {"description", new AttributeValue { S = Request.Form["description"] } },

                        // The date the group was created
                        {"createdDate", new AttributeValue { S = DateTime.Now.ToString() } },

                        // The categories the activity will be listed under
                        {"category", new AttributeValue {S = Request.Form["category"] } },

                        // The latitude and longitude for the activity's location.
                        {"latitude", new AttributeValue {S = Request.Form["latitude"] } },

                        {"longitude", new AttributeValue {S = Request.Form["longitude"] } },

                        // The image for the activity, stored as a base64 string
                        {"avatar", new AttributeValue {S = base64Image} },

                        // The unique ID of the chat assiciated with this activity
                        {"chatId", new AttributeValue { S = chatID } },

                        // The identifier of whether the chat is public (open) or private (closed)
                        {"status", new AttributeValue { S = Request.Form["status"] } },

                        // The maximum amount of people who can join the group
                        {"maxGroupSize", new AttributeValue{ N = Request.Form["maxSize"] } },

                        // The list of banned users
                        {"banned", new AttributeValue{SS = new List<string>{"null"}} },

                        // A complete list of everyone in the group.
                        {"members", new AttributeValue{SS = members} },

                        // The user who created the group
                        {"groupLeader", new AttributeValue{ S = userId } }
                    }
                );

                // Attach a chat to the activity
                // Must use UpdateItemAsync in order to use atomic counter
                await db.client.PutItemAsync(
                    tableName: "RoostChats",
                    item: new Dictionary<string, AttributeValue>
                    {
                        // Primary key: The unique id for the chat
                        {"chatId", new AttributeValue{S = chatID} },

                        // Sort key: The ID of the activity associated with the chat 
                        {"activityId", new AttributeValue{S = activityID} },

                        // Indicate whether there is a poll in progress
                        {"isPollActive", new AttributeValue{BOOL = false} },

                        // The list of users in the chat
                        //{"useridSent", new AttributeValue{SS = users} },

                        // The messages that have been sent
                        //{"messagesSent", new AttributeValue{SS = new List<string>()} },

                        // Unique ids for each message
                        //{"messageIds", new AttributeValue{SS = new List<string>()} },

                        // The current number of messages in the chat (200 max)
                        {"numMessages", new AttributeValue{N = "0"} }

                    }
                );

                //activityIdList.Add(activityID);

                Response.StatusCode = 200;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
        }

        // POST: {activityId}/open
        // Makes a group public
        [HttpPost("{activityId}/open")]
        public async Task<HttpResponseMessage> OpenGroup(string activityId)
        {
            try
            {
                string user = Request.Form["userId"];

                // Get an item from the table.
                var item = await activitiesTable.GetItemAsync(activityId);
                Console.WriteLine("yay");
                Console.WriteLine(user);
                if (item["status"] == "closed" && item["groupLeader"] == user)
                {
                    // Set status to open and update.
                    Console.WriteLine("in if");
                    item["status"] = "open";
                    await activitiesTable.UpdateItemAsync(item);
                    Console.WriteLine("Bitches");
                    Response.StatusCode = 200;
                    HttpResponseMessage response = new HttpResponseMessage();
                    return response;
                }
                else
                {
                    Response.StatusCode = 400;
                    HttpResponseMessage response = new HttpResponseMessage();
                    return response;
                }

            }
            catch (Exception)
            {
                Console.WriteLine("in ex");
                Response.StatusCode = 400;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
        }

        // POST: {activityId}/close
        // Makes a group private
        [HttpPost("{activityId}/close")]
        public async Task<HttpResponseMessage> CloseGroup(string activityId)
        {
            try
            {
                string user = Request.Form["userId"];
                var item = await activitiesTable.GetItemAsync(activityId);

                // Don't try to close a group that already is closed.
                if (item["status"] == "open" && item["groupLeader"] == user)
                {
                    item["status"] = "closed";
                    await activitiesTable.UpdateItemAsync(item);

                    Response.StatusCode = 200;
                    HttpResponseMessage response = new HttpResponseMessage();
                    return response;
                }
                else
                {
                    Response.StatusCode = 400;
                    HttpResponseMessage response = new HttpResponseMessage();
                    return response;
                }

            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
        }

        // POST: /api/activities/{activityId}/deleteactivity
        // Deletes an activity
        [HttpPost("{activityId}/deleteactivity")]
        public async Task<HttpResponseMessage> DeleteActivity(string activityId)
        {
            try
            {
                // id in endpoint is activity id.
                string user = Request.Form["userId"];
                if(user == null) {
                    Response.StatusCode = 400;
                    HttpResponseMessage responsey = new HttpResponseMessage();
                    return responsey;
                }

                // Get the activity from the table
                var activity = await activitiesTable.GetItemAsync(activityId);

                string chatId = activity["chatId"];

                // Only the group leader can delete an activity.
                if (activity["groupLeader"] == user)
                {
                    await chatTable.DeleteItemAsync(chatId, activityId);
                    await activitiesTable.DeleteItemAsync(activityId);
                }
                else {
                    Response.StatusCode = 400;
                    HttpResponseMessage responset = new HttpResponseMessage();
                    return responset;
                }

                Response.StatusCode = 200;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;

            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }

        }

        // POST: /api/activities/join/{id}
        //Join activity and the chat associated with it
        [HttpPost("join/{id}")]
        public async Task<HttpResponseMessage> Join(string id)
        {
            string activityId = id;
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            Console.WriteLine(username);
            Console.WriteLine(password);

            if (activityId != null)
            {
                try
                {
                    var stuff = await activitiesTable.GetItemAsync(id);

                    // The number of people currently in the group and the size limit
                    int numberOfPeeps = stuff["numMembers"].AsInt();
                    int capacity = stuff["maxGroupSize"].AsInt();

                    Console.WriteLine(stuff["members"].AsListOfString());
                    Console.WriteLine(numberOfPeeps);

                    List<string> membersList = stuff["members"].AsListOfString();
                    List<string> banned = new List<string>();

                    if (stuff.ContainsKey("banned"))
                        banned = stuff["banned"].AsListOfString();

                    // don't join if the user is already joined, is banned, or if it's full.
                    if (membersList.Contains(username))
                    {
                        Console.WriteLine("User already added to activity");
                        Response.StatusCode = 400;
                        HttpResponseMessage responsey = new HttpResponseMessage();
                        return responsey;
                    }
                    else if (numberOfPeeps == capacity)
                    {
                        Console.WriteLine("The activity is full");
                        Response.StatusCode = 400;
                        HttpResponseMessage responsey = new HttpResponseMessage();
                        return responsey;
                    }
                    else if (banned.Contains(username))
                    {
                        Console.WriteLine("User is banned");
                        Response.StatusCode = 400;
                        HttpResponseMessage responsey = new HttpResponseMessage();
                        return responsey;
                    }

                    // Add the user to the list and update the entry in the table.
                    membersList.Add(username);
                    Console.WriteLine(stuff["numMembers"].AsInt());

                    numberOfPeeps++;
                    string peopleNum = numberOfPeeps.ToString();

                    stuff["members"] = membersList;
                    stuff["numMembers"] = peopleNum;

                    await activitiesTable.UpdateItemAsync(stuff);

                    Response.StatusCode = 200;
                    HttpResponseMessage response = new HttpResponseMessage();
                    return response;
                }
                catch (Exception)
                {
                    Console.WriteLine("caught exception exception");
                    Response.StatusCode = 400;
                    HttpResponseMessage response = new HttpResponseMessage();
                    return response;
                }
            }
            else
            {
                Response.StatusCode = 400;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
        }
    }
}
