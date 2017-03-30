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
                // Query RoostChats for the list of users
                var chatTable = await db.client.GetItemAsync(
                    tableName: "RoostChats",
                    key: new Dictionary<string, AttributeValue>
                    {
                        // Primary key: The unique id for the chat
                        {"chatId", new AttributeValue{S = Request.Form["chatID"]} },

                        // Sort key: The ID of the activity associated with the chat 
                        {"activityId", new AttributeValue{S = Request.Form["activityID"] } }
                    }
                );

                // Remove the user's id from the list
                List<string> updatedUserList = chatTable.Item["useridSent"].SS;
                updatedUserList.Remove(id);

                // Decrement the number of members in the group and update RoostActivities
                int memberCount = updatedUserList.Count();

                await db.client.PutItemAsync(
                    tableName: "RoostActivities",
                    item: new Dictionary<string, AttributeValue>
                    {
                        // Primary key: The unique activity id, an atomic number concatenated w/userId
                        {"ActivityId", new AttributeValue { S = Request.Form["activityID"] } },

                        // Sort key: The number of people in the group
                        {"numMembers", new AttributeValue { N =  memberCount.ToString() } }
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

        // GET: /api/chat/{id}/threads
        // Gets a list of the threads a user is subscribed to
        [HttpGet("{id}/threads")]
        public IActionResult GetThreads(string id)
        {
            return View();
        }
    }
}
