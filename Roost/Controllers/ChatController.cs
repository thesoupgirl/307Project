using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Amazon.DynamoDBv2.Model;
using Roost;
using System.Diagnostics;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text;
using System.IO;

namespace RoostApp.Controllers
{
    [Route("api/chat")]
    public class ChatController : Controller
    {

        static DBHelper db = new DBHelper();
        static Stopwatch timer = new Stopwatch();
        static string pollId = "ew";
        static StringContent openPoll;
        static StringContent closedPoll;

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
                Console.WriteLine("Is timer running?");
                Console.WriteLine(timer.IsRunning);
                TimeSpan bleh = timer.Elapsed;
                Console.WriteLine(bleh.Seconds);
                int yayMe = bleh.Seconds;
                int minutes = bleh.Minutes;
                if(timer.IsRunning) {
                    TimeSpan ts = timer.Elapsed;
                    Console.WriteLine("timer is running");
                    Console.WriteLine(yayMe);
                    Console.WriteLine("ew");
                    Console.WriteLine(pollId);
                    if(yayMe >= 30 || minutes > 0) {
                        Console.WriteLine("overtime...hammertime?");
                        String username = "lisasoupcampbell@gmail.com";
                        String password = "fuckyou";
                        int killMeNao = pollId.IndexOf("perma");
                        //int meowmeow = pollId.IndexOf("type");
                        Console.WriteLine("please kill me");
                        pollId = pollId.Substring(killMeNao);
                        killMeNao = pollId.IndexOf(":");
                        pollId = pollId.Substring(killMeNao);
                        int meowmeow = pollId.IndexOf(",");
                        Console.WriteLine(pollId.Substring(0, meowmeow));
                        pollId = pollId.Substring(0, meowmeow);
                        pollId = pollId.Substring(1, pollId.Length - 1);
                        pollId = pollId.Substring(1, pollId.Length - 2);
                        Console.WriteLine("jk don't");
                        HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
                        string pollUrl = "https://www.polleverywhere.com/multiple_choice_polls/" + pollId;
                        String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(username + ":" + password));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                        //client.Headers.Add("Authorization", "Basic " + encoded);
                        //client.PutAsync("https://www.polleverywhere.com/multiple_choice_polls/" + pollId + "/results");
                        client.PutAsync(pollUrl, closedPoll);
                        timer.Reset();
                        //var contents = new StringContent("{\"multiple_choice_poll\": {\"id\":14827345,\"updated_at\":\"2016-06-21T07:04:42-07:00\",\"title\":\"I like different titles\",\"opened_at\":null,\"permalink\":\"w8MrohMVVPRUOJx\",\"state\":\"closed\",\"sms_enabled\":false,\"twitter_enabled\":null,\"web_enabled\":false,\"sharing_enabled\":null,\"simple_keywords\":false,\"options\":[{\"id\":77972458,\"value\":\"red\",\"keyword\":null},{\"id\":77972459,\"value\":\"blue\",\"keyword\":null},{\"id\":77972460,\"value\":\"green\",\"keyword\":null}]}}");
                    }
                }
                
                // Most recent version
                Console.WriteLine("Most recent version");
                var item = await chatTable.GetItemAsync(chat, activityId);

                // The indices in all lists correspond to each other.
                List<string> messages = item["messagesSent"].AsListOfString();
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
                        + "\"user\": {\"_id\": \"" + "\","
                        + "\"name\": \"" + "\"" + "}\n";

                    if (i != messages.Count()-1)
                        messageObjects += "},\n";
                }

                messageObjects += "}\n";

                messageObjects += "]}";

                return messageObjects;
            }
            catch (Exception e)
            {
                // Return empty array if no messages found.
                Console.WriteLine(e.Message);
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
                Console.WriteLine("exception caught");
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

        public string ContentToString(HttpContent httpContent)
        {
            var readAsStringAsync = httpContent.ReadAsStringAsync();
            return readAsStringAsync.Result;
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
                string question = Request.Form["question"];
                string response1 = Request.Form["response1"];
                string response2 = Request.Form["response2"];
                string response3 = Request.Form["response3"];
                string response4 = Request.Form["response4"];
                string response5 = Request.Form["response5"];

                Console.WriteLine("before async");
                string authInfo = username + ":" + password;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                Console.WriteLine(client.DefaultRequestHeaders);
                HttpResponseMessage responsey;
                timer.Start();
                Console.WriteLine(timer.IsRunning);
                //check if null
                if(response2 == "") {
                    openPoll = new StringContent("{\"multiple_choice_poll\": {\"id\":null,\"updated_at\":null,\"title\":\"" + question + "\",\"opened_at\":null,\"permalink\":null,\"state\":opened\",\"sms_enabled\":null,\"twitter_enabled\":null,\"web_enabled\":true,\"sharing_enabled\":true,\"simple_keywords\":null,\"options\":[{\"id\":null,\"value\":\"" + response1 + "\",\"keyword\":null}]}}", Encoding.UTF8, "application/json");
                    closedPoll = new StringContent("{\"multiple_choice_poll\": {\"id\":null,\"updated_at\":null,\"title\":\"" + question + "\",\"opened_at\":null,\"permalink\":null,\"state\":closed\",\"sms_enabled\":null,\"twitter_enabled\":null,\"web_enabled\":true,\"sharing_enabled\":true,\"simple_keywords\":null,\"options\":[{\"id\":null,\"value\":\"" + response1 + "\",\"keyword\":null}]}}", Encoding.UTF8, "application/json");  
                    responsey = await client.PostAsync("https://www.polleverywhere.com/multiple_choice_polls", openPoll);
                    //Stream receiveStream = responsey.Content.ReadAsStringAsync();
                    //StreamReader readStream = new StreamReader (receiveStream, Encoding.UTF8);
                    pollId = ContentToString(responsey.Content);
                    Console.WriteLine("argh");
                    Console.WriteLine(pollId);
                    Console.WriteLine("bleh");
                    return responsey;
                }
                else if(response3 == "") {
                    openPoll = new StringContent("{\"multiple_choice_poll\": {\"id\":null,\"updated_at\":null,\"title\":\"" + question + "\",\"opened_at\":null,\"permalink\":null,\"state\":\"opened\",\"sms_enabled\":null,\"twitter_enabled\":null,\"web_enabled\":true,\"sharing_enabled\":null,\"simple_keywords\":null,\"options\":[{\"id\":null,\"value\":\"" + response1 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response2 + "\",\"keyword\":null}]}}", Encoding.UTF8, "application/json"); 
                    closedPoll = new StringContent("{\"multiple_choice_poll\": {\"id\":null,\"updated_at\":null,\"title\":\"" + question + "\",\"opened_at\":null,\"permalink\":null,\"state\":\"closed\",\"sms_enabled\":null,\"twitter_enabled\":null,\"web_enabled\":true,\"sharing_enabled\":null,\"simple_keywords\":null,\"options\":[{\"id\":null,\"value\":\"" + response1 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response2 + "\",\"keyword\":null}]}}", Encoding.UTF8, "application/json"); 
                    responsey = await client.PostAsync("https://www.polleverywhere.com/multiple_choice_polls", openPoll);
                    pollId = ContentToString(responsey.Content);
                    Console.WriteLine("argh");
                    Console.WriteLine(pollId);
                    Console.WriteLine("bleh");
                    return responsey;
                }
                else if(response4 == "") {
                    openPoll = new StringContent("{\"multiple_choice_poll\": {\"id\":null,\"updated_at\":null,\"title\":\"" + question + "\",\"opened_at\":null,\"permalink\":null,\"state\":\"opened\",\"sms_enabled\":null,\"twitter_enabled\":null,\"web_enabled\":true,\"sharing_enabled\":null,\"simple_keywords\":null,\"options\":[{\"id\":null,\"value\":\"" + response1 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response2 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response3 + "\",\"keyword\":null}]}}", Encoding.UTF8, "application/json"); 
                    closedPoll = new StringContent("{\"multiple_choice_poll\": {\"id\":null,\"updated_at\":null,\"title\":\"" + question + "\",\"opened_at\":null,\"permalink\":null,\"state\":\"closed\",\"sms_enabled\":null,\"twitter_enabled\":null,\"web_enabled\":true,\"sharing_enabled\":null,\"simple_keywords\":null,\"options\":[{\"id\":null,\"value\":\"" + response1 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response2 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response3 + "\",\"keyword\":null}]}}", Encoding.UTF8, "application/json"); 
                    responsey = await client.PostAsync("https://www.polleverywhere.com/multiple_choice_polls", openPoll);
                    pollId = ContentToString(responsey.Content);
                    Console.WriteLine("argh");
                    Console.WriteLine(pollId);
                    Console.WriteLine("bleh");
                    return responsey;
                }
                else if(response5 == "") {
                    openPoll = new StringContent("{\"multiple_choice_poll\": {\"id\":null,\"updated_at\":null,\"title\":\"" + question + "\",\"opened_at\":null,\"permalink\":null,\"state\":\"opened\",\"sms_enabled\":null,\"twitter_enabled\":null,\"web_enabled\":true,\"sharing_enabled\":null,\"simple_keywords\":null,\"options\":[{\"id\":null,\"value\":\"" + response1 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response2 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response3 + "\",\"keyword\":null}, {\"id\":null,\"value\":\"" + response4 + "\",\"keyword\":null}]}}", Encoding.UTF8, "application/json"); 
                    closedPoll = new StringContent("{\"multiple_choice_poll\": {\"id\":null,\"updated_at\":null,\"title\":\"" + question + "\",\"opened_at\":null,\"permalink\":null,\"state\":\"closed\",\"sms_enabled\":null,\"twitter_enabled\":null,\"web_enabled\":true,\"sharing_enabled\":null,\"simple_keywords\":null,\"options\":[{\"id\":null,\"value\":\"" + response1 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response2 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response3 + "\",\"keyword\":null}, {\"id\":null,\"value\":\"" + response4 + "\",\"keyword\":null}]}}", Encoding.UTF8, "application/json"); 
                    responsey = await client.PostAsync("https://www.polleverywhere.com/multiple_choice_polls", openPoll);
                    pollId = ContentToString(responsey.Content);
                    Console.WriteLine("argh");
                    Console.WriteLine(pollId);
                    Console.WriteLine("bleh");
                    return responsey;
                }
                else {
                    openPoll = new StringContent("{\"multiple_choice_poll\": {\"id\":null,\"updated_at\":null,\"title\":\"" + question + "\",\"opened_at\":null,\"permalink\":null,\"state\":\"opened\",\"sms_enabled\":null,\"twitter_enabled\":null,\"web_enabled\":true,\"sharing_enabled\":null,\"simple_keywords\":null,\"options\":[{\"id\":null,\"value\":\"" + response1 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response2 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response3 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response4 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response5 + "\",\"keyword\":null}]}}", Encoding.UTF8, "application/json"); 
                    closedPoll = new StringContent("{\"multiple_choice_poll\": {\"id\":null,\"updated_at\":null,\"title\":\"" + question + "\",\"opened_at\":null,\"permalink\":null,\"state\":\"closed\",\"sms_enabled\":null,\"twitter_enabled\":null,\"web_enabled\":true,\"sharing_enabled\":null,\"simple_keywords\":null,\"options\":[{\"id\":null,\"value\":\"" + response1 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response2 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response3 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response4 + "\",\"keyword\":null},{\"id\":null,\"value\":\"" + response5 + "\",\"keyword\":null}]}}", Encoding.UTF8, "application/json"); 
                    responsey = await client.PostAsync("https://www.polleverywhere.com/multiple_choice_polls", openPoll);
                    pollId = ContentToString(responsey.Content);
                    Console.WriteLine("argh");
                    Console.WriteLine(pollId);
                    Console.WriteLine("bleh");
                    return responsey;
                }
                //Stream receiveStream = response.GetResponseStream ();
                //StreamReader readStream = new StreamReader (receiveStream, Encoding.UTF8);
                //string blah = readStream.ReadToEnd();
                Console.WriteLine("meow");
                Console.WriteLine(responsey.Content);
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
        [HttpPost("{activityId}/leave")]
        public async Task<HttpResponseMessage> LeaveGroup(string activityId)
        {
            try
            {
                // Activity id comes from route.
                string user = Request.Form["userID"];

                var item = await activitiesTable.GetItemAsync(activityId);

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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                List<string> dates = new List<string>();

                if (item.ContainsKey("messagesSent"))
                    messages = item["messagesSent"].AsListOfString();

                if (item.ContainsKey("timestamps"))
                    dates = item["timestamps"].AsListOfString();

                // Add the message's info
                Console.WriteLine(Request.Form["message"]);
                messages.Add(Request.Form["message"]);
                dates.Add(DateTime.Now.ToString());

                item["messagesSent"] = messages;
                item["timestamps"] = dates;

                // Update the table
                await chatTable.UpdateItemAsync(item);

                Response.StatusCode = 200;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Response.StatusCode = 400;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
}
    }
}
