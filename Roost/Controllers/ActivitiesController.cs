using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Amazon.DynamoDBv2.Model;
using Roost;
using Microsoft.Extensions.Primitives;

namespace RoostApp.Controllers
{
    [Route("api/activities")]
    public class ActivitiesController : Controller
    {

        DBHelper db = new DBHelper();

        // GET: /api/activities/{id}/{dist}
        // Gets list of activities within certain radius of user
        [HttpGet("{id}/{dist}")]
        public IActionResult FindActivities(string id, string dist)
        {
            return View();
        }

        // GET: /api/activities/category/{id}/{dist}
        // Gets list of activities within certain radius of user by category
        [HttpGet("{id}/{dist}")]
        public async Task<HttpResponseMessage> FindActivitiesByCategory(string id, string dist)
        {
            try
            {
                var r = await db.client.GetItemAsync(
                    tableName: "RoostActivities",
                    key: new Dictionary<string, AttributeValue>
                    {
                        // Primary key: The unique activity id
                        {"ActivityId", new AttributeValue { S = id} }, 

                        // Sort key: The number of members in the group
                        {"numMembers", new AttributeValue { N = Request.Form["numMembers"]} },

                        // The categories the activity will be listed under
                        {"categories", new AttributeValue {SS = Request.Form["categories"].ToList<string>() } }
                    }
                );
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

        // POST: /api/activities/{id}/createactivity
        // Creates an activity
        [HttpPost("{id}/createactivity")]
        public async Task<HttpResponseMessage> CreateActivity(string id)
        {

            try
            {
                string base64Image = null;

                // Take the uploaded avatar and convert it to a base64 string.
                // This is how the images will be stored in the table.
                if (!String.IsNullOrEmpty(Request.Form["avatar"]))
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(Request.Form["avatar"]);
                    base64Image = Convert.ToBase64String(imageArray);
                }


                // Add the activity to the database table
                await db.client.PutItemAsync(
                    tableName: "RoostActivities",
                    item: new Dictionary<String, AttributeValue>
                    {
                        // Primary key: The unique activity id
                        {"ActivityId", new AttributeValue { S = id} }, 

                        // Sort key: The number of members in the group
                        {"numMembers", new AttributeValue { N = Request.Form["numMembers"]} },

                        // The date the group was created
                        {"createdDate", new AttributeValue { S = System.DateTime.Today.ToString()}  },

                        // The categories the activity will be listed under
                        {"categories", new AttributeValue {SS = Request.Form["categories"].ToList<string>() } },

                        // The latitude and longitude for the activity's location.
                        {"latitude", new AttributeValue {S = Request.Form["latitude"] } },

                        {"longitude", new AttributeValue {S = Request.Form["longitude"] } },

                        // The image for the activity, stored as a base64 string
                        {"avatar", new AttributeValue {S = base64Image} },

                        // The name of the group
                        {"name", new AttributeValue { S = Request.Form["name"] } },

                        // The description of the group
                        {"description", new AttributeValue { S = Request.Form["description"] } },

                        // The unique ID of the chat assiciated with this activity
                        {"chatId", new AttributeValue { S = id } },

                        // The identifier of whether the chat is public (open) or private (closed)
                        {"status", new AttributeValue { S = Request.Form["status"]} },

                        // The maximum amount of people who can join the group
                        {"maxGroupSize", new AttributeValue{ N = Request.Form["maxSize"]} },

                        // The userId of the person who created the group
                        {"groupLeader", new AttributeValue{ S = Request.Form["groupLeader"]} }
                    }
                );

                // This list will store the userIds of all members in the activity
                List<string> users = new List<string>();

                // Add the activity's creator to the list
                users.Add(Request.Form["groupLeader"]);

                // Attach a chat to the activity
                await db.client.PutItemAsync(
                    tableName: "RoostChats",
                    item: new Dictionary<string, AttributeValue>
                    {
                        // Primary key: The unique id for the chat
                        {"chatId", new AttributeValue{S = id} },

                        // Sort key: The ID of the activity associated with the chat 
                        {"groupId", new AttributeValue{S = id} },

                        // Indicate whether there is a poll in progress
                        {"isPollActive", new AttributeValue{BOOL = false} },

                        // The list of users in the chat
                        {"useridSent", new AttributeValue{SS = users} },

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

        // POST: /api/activities/{id}/deleteactivity
        // Deletes an activity
        [HttpPost("{id}/deleteactivity")]
        public IActionResult DeleteActivity(string id)
        {
            return View();
        }
    }
}
