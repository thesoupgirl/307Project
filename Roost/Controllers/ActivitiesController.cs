using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Amazon.DynamoDBv2.Model;
using Roost;
using Amazon.DynamoDBv2.DocumentModel;

namespace RoostApp.Controllers
{
    [Route("api/activities")]
    public class ActivitiesController : Controller
    {

        static DBHelper db = new DBHelper();

        Table activitiesTable = Table.LoadTable(db.client, "RoostActivities");

        // A list of all the activity ids that will be used in searching
        List<string> activityIdList = new List<string>();

        // GET: /api/activities/{id}/{dist}
        // Gets list of activities within certain radius of user
        [HttpGet("{id}/{dist}")]
        public string FindActivities(string id, string dist)
        {
            try
            {
                return ""; 
            }
            catch (Exception)
            {
                return "";
            }
        }

        // GET: /api/activities/category/{id}/{dist}
        // Gets list of activities within certain radius of user by category
        // TODO: needs to return JSON string
        [HttpGet("{id}/{dist}")]
        public async Task<string> FindActivitiesByCategory(string id, string dist)
        {
            try
            {
                // Frontend will be sending user id and category
                string category = Request.Form["category"];

                // Activity cannot be full or closed (hidden) and the user must not be in it.

                // The key used to find the activities
                Dictionary<string, AttributeValue> searchKey = 
                    new Dictionary<string, AttributeValue>
                    {
                        {"ActivityId", new AttributeValue { S = id } },
                        {"category", new AttributeValue { S = category } },
                        {"status", new AttributeValue { S = "open" } }
                    };

                // Create a new list to store the keys
                List<Dictionary<string, AttributeValue>> keys =
                    new List<Dictionary<string, AttributeValue>>
                    {
                        searchKey
                    };

                // Create a new key for every activity id

                // Then run BatchGetItemAsync


                var resp = await db.client.BatchGetItemAsync(
                    requestItems: new Dictionary<string, KeysAndAttributes>
                    {
                        { "RoostActivities", new KeysAndAttributes { Keys = keys } }
                    }
                );

                
                
                var result = resp.Responses["RoostActivities"];
                return result.ToString();
               
            } catch (Exception)
            {
                return "No items found.";
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
                } else
                {
                    base64Image = "none";
                }

                List<string> members = new List<string> { id };

                // Add the activity to the database table
                await db.client.PutItemAsync(
                    tableName: "RoostActivities",
                    item: new Dictionary<String, AttributeValue>
                    {
                        // Primary key: The unique activity id, an atomic number concatenated w/userId
                        {"ActivityId", new AttributeValue { S = activityID } },

                        // The number of people in the group
                        {"numMembers", new AttributeValue { N = "1" } },

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

                        // The userId of the person who created the group
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

                activityIdList.Add(activityID);

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
                string activityId = Request.Form["activityId"];

                // Get an item from the table.
                var item = await activitiesTable.GetItemAsync(activityId);

                if (item["status"] == "closed")
                {
                    // Set status to open and update.
                    item["status"] = "open";
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

        // POST: {id}/close
        // Makes a group private
        [HttpPost("{id}/close")]
        public async Task<HttpResponseMessage> CloseGroup(string id)
        {
            try
            {
                string activityId = Request.Form["activityId"];
                var item = await activitiesTable.GetItemAsync(activityId);

                // Don't try to close a group that already is closed.
                if (item["status"] == "open")
                {
                    item["status"] = "closed";
                    await activitiesTable.UpdateItemAsync(item);

                    Response.StatusCode = 200;
                    HttpResponseMessage response = new HttpResponseMessage();
                    return response;
                } else
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
                Dictionary<string, AttributeValue> activityTableKey = 
                    new Dictionary<string, AttributeValue>
                    {
                        {"ActivityId", new AttributeValue{ S = Request.Form["activityId"] } }
                    };


                // Get the activity from the table
                var activity = await db.client.GetItemAsync(tableName: "RoostActivities", key: activityTableKey);

                // Only delete the activity if the id matches that of the group leader
                if (activity.Item["groupLeader"].S == id)
                    await db.client.DeleteItemAsync(tableName: "RoostActivities", key: activityTableKey);

                Response.StatusCode = 200;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;

            } catch (Exception)
            {
                Response.StatusCode = 400;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
            
        }
    }
}
