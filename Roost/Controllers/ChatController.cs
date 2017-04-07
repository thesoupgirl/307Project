using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Amazon.DynamoDBv2.Model;
using Roost;

namespace RoostApp.Controllers
{
    [Route("api/chat")]
    public class ChatController : Controller
    {

        DBHelper db = new DBHelper();

        // GET: /api/chat/{id}/{chat}/messages
        // Gets messages for a thread
        [HttpGet("{id}/{chat}/messages")]
        public IActionResult GetMessages(string id, string chat)
        {
            return View();
        }

        // POST: /api/chat/{id}/send
        // Sends message to the group
        [HttpPost("{id}/send")]
        public IActionResult SendMessage(string id)
        {
            return View();
        }

        // POST: /api/chat/{id}/leave
        // Removes a user from the chat
        [HttpPost("{id}/leave")]
        public async Task<HttpResponseMessage> LeaveGroup(string id)
        {
            try
            {
                // Activity id comes from route.
                string user = Request.Form["userID"];

                Dictionary<string, AttributeValue> activityTableKey =
                    new Dictionary<string, AttributeValue>
                    {
                        {"ActivityId", new AttributeValue { S = id } }
                    };

                // Get the activity from its table along with the chat id
                var activity = await db.client.GetItemAsync(tableName: "RoostActivities", key: activityTableKey);

                // Remove the user's id from the list
                List<string> updatedUserList = activity.Item["members"].SS;

                updatedUserList.Remove(user);

                // Update the activity's member count and Put the updated list back in the table
                await db.client.UpdateItemAsync(tableName: "RoostActivities", key: activityTableKey,

                    attributeUpdates: new Dictionary<string, AttributeValueUpdate>
                    {
                        {
                            "numMembers",
                            new AttributeValueUpdate {Action = "ADD", Value = new AttributeValue { N = "-1" } }
                        },
                        {
                            "members",
                            new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue { SS = updatedUserList } }
                        }
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

        // GET: /api/chat/{id}/threads
        // Gets a list of the threads a user is subscribed to
        [HttpGet("{id}/threads")]
        public IActionResult GetThreads(string id)
        {
            return View();
        }
    }
}
