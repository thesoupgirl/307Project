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

        // A list of all the activity ids that will be used in searching
        //List<string> activityIdList = new List<string>();

        // GET: /api/activities/{id}/{dist}
        // Gets list of activities within certain radius of user based on popularity
        // TODO: determine the criteria for popularity.
        [HttpGet("{id}/{dist}/search")]
        public async Task<string> FindActivities(string id, string dist)
        {
            try
            {
                // Pull the top 25 most popular
                ScanFilter scanFilter = new ScanFilter();
                scanFilter.AddCondition("status", ScanOperator.Equal, "open");

                // Start the search
                Search search = activitiesTable.Scan(scanFilter);

                // Put all results into a list.
                List<Document> docList = new List<Document>();

                // TODO: if user in in group, remove from list.
                // TODO: sort by numMembers
                //List<string> results = new List<string>();
                string fuckThis = "{ \"data\": [ ";
                do
                {
                    docList = await search.GetNextSetAsync();

                    foreach (Document d in docList)
                    {
                        // Get the list and number of members and max size.
                        /*List<string> members = d["members"].AsListOfString();
                        int numMembers = d["numMembers"].AsInt();
                        int max = d["maxGroupSize"].AsInt();*/

                        // Remove the activity if it's full and has the user in it.
                        //if ((numMembers < max) && !members.Contains(id))
                        //{   // append all items in ToJsonPretty form as one string and return the result
                        fuckThis = fuckThis + d.ToJson().ToString() + " , ";
                        //Console.WriteLine("first time: " + fuckThis);
                        //fuckThis.Replace("\\", "");
                        //Console.WriteLine("Second time: " + fuckThis);
                        //results.Add(fuckThis);
                        //}
                    }
                } while (!search.IsDone);
                        fuckThis = fuckThis.Remove(fuckThis.Length - 3);
                        fuckThis = fuckThis + " ] }";
                        fuckThis = fuckThis.Replace("\\", "");
                        Console.WriteLine("Second time: " + fuckThis);
                        //results.Add(fuckThis);
                return fuckThis;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // POST: /api/activities/{id}/createactivity
        // Creates an activity
        [HttpPost("{id}/createactivity")]
        public async Task<HttpResponseMessage> CreateActivity(string id)
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

                List<string> members = new List<string> { id };

                // Don't let them create the activity just for themselves.
                if (Request.Form["maxSize"] == 1)
                {
                    //Response.StatusCode = 400;
                    //HttpResponseMessage response = new HttpResponseMessage();
                    //return response;
                }

                // Add the activity to the database table
                await db.client.PutItemAsync(
                    tableName: "RoostActivities",
                    item: new Dictionary<String, AttributeValue>
                    {
                        // Primary key: The unique activity id, an atomic number concatenated w/userId
                        {"ActivityId", new AttributeValue { S = activityID } },

                        // The number of people in the group
                        {"numMembers", new AttributeValue { N = members.Count().ToString() } },

                        // The name of the group
                        {"name", new AttributeValue { S = Request.Form["name"] } },

                        // The description of the group
                        {"description", new AttributeValue { S = Request.Form["description"] } },

                        // The date the group was created
                        {"createdDate", new AttributeValue { S = DateTime.Today.ToString() } },

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

                        // A complete list of everyone in the group.
                        {"members", new AttributeValue{SS = members} },

                        // The user who created the group
                        {"groupLeader", new AttributeValue{ S = id } }
                    }
                );

                // This list will store the userIds of all members in the activity
                // Add the activity's creator to the list
                //List<string> users = new List<string> { id };

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

                        // Links for pictures sent in the chat
                        //{"picLinks", new AttributeValue{SS = new List<string>()} },

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

        // POST: {id}/open
        // Makes a group public
        [HttpPost("{id}/open")]
        public async Task<HttpResponseMessage> OpenGroup(string id)
        {
            try
            {
                string user = Request.Form["userId"];

                // Get an item from the table.
                var item = await activitiesTable.GetItemAsync(id);

                if (item["status"] == "closed" && item["groupLeader"] == user)
                {
                    // Set status to open and update.
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
                Response.StatusCode = 400;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
        }

        // POST: {id}/close
        // Makes a group private
        [HttpPost("{id}/close")]
        public async Task<HttpResponseMessage> CloseGroup(string id)
        {
            try
            {
                string user = Request.Form["userId"];
                var item = await activitiesTable.GetItemAsync(id);

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

        // POST: /api/activities/{id}/deleteactivity
        // Deletes an activity
        [HttpPost("{id}/deleteactivity")]
        public async Task<HttpResponseMessage> DeleteActivity(string id)
        {
            try
            {
                // id in endpoint is activity id.
                string user = Request.Form["userId"];

                // Get the activity from the table
                var activity = await activitiesTable.GetItemAsync(id);

                string chatId = activity["chatId"];

                // Only the group leader can delete an activity.
                if (activity["groupLeader"] == user)
                {
                    await chatTable.DeleteItemAsync(chatId, id);
                    await activitiesTable.DeleteItemAsync(id);
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

            if(activityId != null) {
                try {
                    GetItemResponse stuff = await db.client.GetItemAsync(
                        tableName: "RoostActivities",
                        key: new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
                        {
                            {"ActivityId", new AttributeValue {S = id} }
                        }
                    );

                    Console.WriteLine(stuff.Item["members"].SS);
                    Console.WriteLine(stuff.Item["numMembers"].N);
                    List<string> membersList = stuff.Item["members"].SS;
                    if(membersList.Contains(username)) {
                        Console.WriteLine("User already added to activity");
                        Response.StatusCode = 400;
                        HttpResponseMessage responsey = new HttpResponseMessage();
                        return responsey;
                    }
                    membersList.Add(username);
                    Console.WriteLine(stuff.Item["numMembers"].N);
                    int numberOfPeeps = Convert.ToInt32(stuff.Item["numMembers"].N);
                    numberOfPeeps++;
                    string peopleNum = numberOfPeeps.ToString();

                    await db.client.UpdateItemAsync(
                    tableName: "RoostActivities",
                    key: new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
                    {
                        {"ActivityId", new AttributeValue {S = id} }
                    },

                    attributeUpdates: new Dictionary<string, AttributeValueUpdate>
                    {
                            {"members", new AttributeValueUpdate(new AttributeValue {SS = membersList}, AttributeAction.PUT)},
                            {"numMembers", new AttributeValueUpdate(new AttributeValue {N = peopleNum}, AttributeAction.PUT)}
                    }
                );

                Response.StatusCode = 200;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
                }
                catch(Exception) {
                    Console.WriteLine("caught exception exception");
                    Response.StatusCode = 400;
                    HttpResponseMessage response = new HttpResponseMessage();
                    return response;
                }
            }
            else {
                Response.StatusCode = 400;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
        }
    }
}
