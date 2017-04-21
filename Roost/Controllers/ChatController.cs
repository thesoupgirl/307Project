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
            try
            {
                var item = await chatTable.GetItemAsync(chat, activityId);

                // The indices in all lists correspond to each other.
                List<string> messages = item["messagesSent"].AsListOfString();
                List<string> users = item["userIdSent"].AsListOfString();
                List<string> dates = item["timestamps"].AsListOfString();


                // The format required by the React module is a list of message objects.
                string messageObjects = "{ \"messages\": [";

                for (int i = 0; i < messages.Count(); i++)
                {
                    // Parse the date
                    DateTime time = Convert.ToDateTime(dates.ElementAt(i));

                    // Assemble the string
                    messageObjects += "{\n";

                    messageObjects = messageObjects + "\"_id\": \"" + i + "\",\n"
                        + "\"text\": \"" + messages.ElementAt(i) + "\",\n"
                        + "\"createdAt\": \"new Date(Date.UTC(" + time.Year + "," + time.Month + "," + time.Day + "," + time.Hour + "," + time.Minute + "," + time.Second + "))\",\n"
                        + "\"user\": {\"_id\": \"" + users.ElementAt(i) + "\","
                        + "\"name\": \"" + users.ElementAt(i) + "\"" + "}\n";

                    if (i != messages.Count()-1)
                        messageObjects += "},\n";
                }

                messageObjects += "}\n";

                messageObjects += "]}";

                return messageObjects;
            }
            catch (Exception)
            {
                // Return empty array if no messages found.
                return "{ \"messages\": []}";
            }
        }

        // GET: /api/chat/{activityId}/users
        // gets all users in a chat
        [HttpGet("{activityId}/users")]
        public async Task<List<string>> GetUsers(string activityId)
        {
            try
            {
                var item = await activitiesTable.GetItemAsync(activityId);
                return item["members"].AsListOfString();
            } catch (Exception)
            {
                Console.WriteLine("exeption caught");
                return null;
            }
            
        }

        // GET: /api/chat/{activityId}/usercount
        // gets number of users in a chat
        [HttpGet("{activityId}/usercount")]
        public async Task<int> GetUserCount(string activityId)
        {
            List<string> users = await GetUsers(activityId);
            return users.Count();
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
        [HttpPost("{activityId}/leave")]
        public async Task<HttpResponseMessage> LeaveGroup(string activityId)
        {
            try
            {
                // Activity id comes from route.
                string user = Request.Form["userID"];

                var item = await activitiesTable.GetItemAsync(activityId);

                //Dictionary<string, AttributeValue> activityTableKey =
                //    new Dictionary<string, AttributeValue>
                //    {
                //        {"ActivityId", new AttributeValue { S = activityId } }
                //    };

                //// Get the activity from its table along with the chat id
                //var activity = await db.client.GetItemAsync(tableName: "RoostActivities", key: activityTableKey);

                if (user == item["groupLeader"].AsString())
                {
                    Response.StatusCode = 400;
                    HttpResponseMessage respons = new HttpResponseMessage();
                    return respons;
                }

                // Remove the user's id from the list
                List<string> updatedUserList = item["members"].AsListOfString();

                updatedUserList.Remove(user);

                // Update the activity's member count and Put the updated list back in the table
                item["members"] = updatedUserList;
                item["numMembers"] = updatedUserList.Count();

                await activitiesTable.UpdateItemAsync(item);

                
                //await db.client.UpdateItemAsync(tableName: "RoostActivities", key: activityTableKey,

                //    attributeUpdates: new Dictionary<string, AttributeValueUpdate>
                //    {
                //        {
                //            "numMembers",
                //            new AttributeValueUpdate {Action = "ADD", Value = new AttributeValue { N = "-1" } }
                //        },
                //        {
                //            "members",
                //            new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue { SS = updatedUserList } }
                //        }
                //    }
                    
                //);

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

        // POST /api/chat/{activityId}/kick
        // Kicks a user from the group
        // Only the group leader has this ability
        [HttpPost("{activityId}/kick")]
        public async Task<HttpResponseMessage> KickUser(string activityId)
        {
            var item = await activitiesTable.GetItemAsync(activityId);

            try
            {
                string userToKick = Request.Form["userID"];

                await LeaveGroup(activityId);

                List<string> bannedUsers = new List<string>();

                if (item.ContainsKey("banned"))
                    bannedUsers = item["banned"].AsListOfString();

                // Add the kicked user to that list.
                if (!bannedUsers.Contains(userToKick))
                {
                    bannedUsers.Add(userToKick);
                    item["banned"] = bannedUsers;
                    await activitiesTable.UpdateItemAsync(item);
                }

                Response.StatusCode = 200;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
            catch (Exception)
            {
                Console.WriteLine("exception");
                Response.StatusCode = 400;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
        }

        // POST /api/chat/{activityId}/{chatId}/sendinvite
        // Body will send column names that a message contains
        // add it to the list of messages in an activity
        // message will be "x invited y"
        [HttpPost("{activityId}/{chatId}/addinvite")]
        public async Task<HttpResponseMessage> AddInvite(string activityId, string chatId)
        {
            try
            {
                // Get the lists from the table
                var item = await chatTable.GetItemAsync(chatId, activityId);


                List<string> messages = new List<string>();
                List<string> users = new List<string>();
                List<string> dates = new List<string>();

                if (item.ContainsKey("messagesSent"))
                {
                    messages = item["messagesSent"].AsListOfString();
                    users = item["userIdSent"].AsListOfString();
                    dates = item["timestamps"].AsListOfString();
                }
                
                // Add the message's info
                messages.Add(Request.Form["message"]);
                users.Add(Request.Form["user"]);
                dates.Add(DateTime.Now.ToString());

                item["messagesSent"] = messages;
                item["userIdSent"] = users;
                item["timestamps"] = dates;

                // Update the table
                await chatTable.UpdateItemAsync(item);

                Response.StatusCode = 200;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
            catch (Exception)
            {
                Console.WriteLine("exception");
                Response.StatusCode = 400;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
}
    }
}
