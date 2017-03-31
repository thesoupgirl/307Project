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
                Dictionary<string, AttributeValue> chatTableKey =
                    new Dictionary<string, AttributeValue>
                    {
                        {"chatId", new AttributeValue{S = Request.Form["chatID"]} },
                        {"activityId", new AttributeValue{S = Request.Form["activityID"] } }
                    };

                // Query RoostChats for the list of users
                var chatTable = await db.client.GetItemAsync(tableName: "RoostChats", key: chatTableKey);

                // Remove the user's id from the list
                List<string> updatedUserList = chatTable.Item["useridSent"].SS;
                int memberCount = updatedUserList.Count();

                updatedUserList.Remove(id);

                Dictionary<string, AttributeValue> activityTableKey =
                    new Dictionary<string, AttributeValue>
                    {
                        {"ActivityId", new AttributeValue { S = Request.Form["activityID"] } },
                        {"numMembers", new AttributeValue { N =  memberCount.ToString() } }
                    };

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

                // Since the member count is a sort key and cannot be updated with UpdateItemAsync, 
                // the server will get the activity from the table, delete it, decrement the count, and then
                // Add the updated value back to the table.
                var activity = await db.client.GetItemAsync(tableName: "RoostActivities", key: activityTableKey);

                await db.client.DeleteItemAsync(tableName: "RoostActivities", key: activityTableKey);

                memberCount--;
                activity.Item["numMembers"].N = memberCount.ToString();
                
                await db.client.PutItemAsync(tableName: "RoostActivities", item: activity.Item);

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
