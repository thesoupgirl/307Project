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
    [Route("api/chat")]
    public class ChatController : Controller
    {

        static DBHelper db = new DBHelper();

        // Initialize the tables as Table objects
        Table activitiesTable = Table.LoadTable(db.client, "RoostActivities");
        Table chatTable = Table.LoadTable(db.client, "RoostChats");

        // GET: /api/chat/{id}/{chat}/messages
        // Gets messages for a thread
        [HttpGet("{activityId}/{chat}/messages")]
        public async Task<string> GetMessages(string activityId, string chat)
        {
            var item = await chatTable.GetItemAsync(chat,activityId);

            // The indices in all lists correspond to each other.
            List<string> messages = item["messagesSent"].AsListOfString();
            List<string> users = item["userIdSent"].AsListOfString();
            List<string> dates = item["timestamps"].AsListOfString();


            // The format required by the React module is a list of message objects.
            string messageObjects = "messages: [\n";
            
            for (int i = 0; i < messages.Count(); i++)
            {
                // Parse the date
                DateTime time = Convert.ToDateTime(dates.ElementAt(i));

                // Assemble the string
                messageObjects += "{\n";

                messageObjects = messageObjects + "_id: " + i + ",\n"
                    + "text: '" + messages.ElementAt(i) + "',\n"
                    + "createdAt: new Date(Date.UTC("+time.Year+","+time.Month+","+time.Day+","+time.Hour+","+time.Minute+","+time.Second+")),\n"
                    + "user: {_id: " + users.ElementAt(i) + ","
                    + "name: '" + users.ElementAt(i) + "'," + "},\n";

                messageObjects += "},\n";
            }

            messageObjects += "],";

            return messageObjects;
        }

        // GET: /api/chat/{id}/users
        // gets all users in a chat
        [HttpGet("{activityId}/users")]
        public async Task<List<string>> GetUsers(string activityId)
        {
            var item = await activitiesTable.GetItemAsync(activityId);
            return item["members"].AsListOfString();
        }

        // POST: /api/chat/{id}/send
        // Sends message to the group
        [HttpPost("{id}/send")]
        public IActionResult SendMessage(string id)
        {
            // add to messagesSent list
            // increment numMessages
            // if list size is 200, delete the least recent message.
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

                if (id == activity.Item["groupLeader"].S)
                {
                    Response.StatusCode = 400;
                    HttpResponseMessage respons = new HttpResponseMessage();
                    return respons;
                }

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
