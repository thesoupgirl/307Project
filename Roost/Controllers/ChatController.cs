using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Amazon.DynamoDBv2.Model;
using Roost;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text;

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

        // POST: /api/chat/createpoll
        // Creates a poll
        [HttpPost("createpoll")]
        public async Task<HttpResponseMessage> CreatePoll() 
        {
            try {
                String username = "lisasoupcampbell@gmail.com";
                String password = "fuckyou";
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(username + ":" + password));
                //client.Headers.Add("Authorization", "Basic " + encoded);
                HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));

                //get stuff from form
                string question = "what day is it?";
                Console.WriteLine("before async");
                string authInfo = username + ":" + password;
                //authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                //req.Headers["Authorization"] = "Basic " + authInfo;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                //var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                Console.WriteLine(client.DefaultRequestHeaders);
                var contents = new StringContent("{\"multiple_choice_poll\": {\"id\":null,\"updated_at\":null,\"title\":\"" + question + "\",\"opened_at\":null,\"permalink\":null,\"state\":null,\"sms_enabled\":null,\"twitter_enabled\":null,\"web_enabled\":null,\"sharing_enabled\":null,\"simple_keywords\":null,\"options\":[{\"id\":null,\"value\":\"red\",\"keyword\":null},{\"id\":null,\"value\":\"blue\",\"keyword\":null},{\"id\":null,\"value\":\"green\",\"keyword\":null}]}}", Encoding.UTF8, "application/json"); 
                //Console.WriteLine(contents.ToString());
                //HttpResponseMessage responsey = await client.PostAsync("https://www.polleverywhere.com/multiple_choice_polls", contents);
                HttpResponseMessage responsey = client.PostAsync("https://www.polleverywhere.com/multiple_choice_polls", contents).Result;
                Console.WriteLine(responsey);
                Console.WriteLine("after client post async");
                Response.StatusCode = 200;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;

            }
            catch(Exception) {
                Console.WriteLine("caught exception");
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
