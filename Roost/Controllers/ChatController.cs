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

        // POST: /api/chat/{id}/join
        // Add a user to the chat
        [HttpPost("{id}/join")]
        public async Task<HttpResponseMessage> JoinGroup(string id)
        {
            try
            {
                // Get the activity and it's chatId from the activities table.
                // Increase member count
                // Add userId to the list in chats table
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

        // POST: /api/chat/{id}/leave
        // Removes a user from the chat
        [HttpPost("{id}/leave")]
        public async Task<HttpResponseMessage> LeaveGroup(string id)
        {
            try
            {
                string activityId = Request.Form["activityID"];

                Dictionary<string, AttributeValue> activityTableKey =
                    new Dictionary<string, AttributeValue>
                    {
                        {"ActivityId", new AttributeValue { S = activityId } }
                    };

                // Get the activity from its table along with the chat id
                var activity = await db.client.GetItemAsync(tableName: "RoostActivities", key: activityTableKey);

                string chatId = activity.Item["chatId"].S;

                // Get the chat from its table and remove the user.
                Dictionary<string, AttributeValue> chatTableKey =
                    new Dictionary<string, AttributeValue>
                    {
                        {"chatId", new AttributeValue{S = chatId} },
                        {"activityId", new AttributeValue{S = activityId } }
                    };

                // Query RoostChats for the list of users
                var chatTable = await db.client.GetItemAsync(tableName: "RoostChats", key: chatTableKey);

                // Remove the user's id from the list
                List<string> updatedUserList = chatTable.Item["useridSent"].SS;

                updatedUserList.Remove(id);

                // Update the activity's member count
                await db.client.UpdateItemAsync(
                    tableName: "RoostActivities",
                    key: activityTableKey,

                    attributeUpdates: new Dictionary<string, AttributeValueUpdate>
                    {
                        {
                            "numMembers",
                            new AttributeValueUpdate {Action = "ADD", Value = new AttributeValue { N = "-1" } }
                        }
                    }
                );

                // Put the updated list back in the table
                await db.client.UpdateItemAsync(
                    tableName: "RoostChats",
                    key: chatTableKey,

                    attributeUpdates: new Dictionary<string, AttributeValueUpdate>
                    {
                        {
                            "useridSent",
                            new AttributeValueUpdate {Action = "PUT", Value = new AttributeValue { SS = updatedUserList } }
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
