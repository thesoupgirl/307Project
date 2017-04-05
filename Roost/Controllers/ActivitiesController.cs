using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Roost.Interfaces;
using Roost.Models;
using System.IO;
using System.Text;

namespace Roost.Controllers
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
        public IActionResult FindActivitiesByCategory(string id, string dist)
        {
            return View();
        }

        // POST: /api/activities/{id}/createactivity
        // Creates an activity
        [HttpPost("{id}/createactivity")]
        public IActionResult CreateActivity(string id)
        {
            return View();
        }

        // POST: /api/activities/{id}/deleteactivity
        // Deletes an activity
        [HttpPost("{id}/deleteactivity")]
        public IActionResult DeleteActivity(string id)
        {
            return View();
        }
        // POST: /api/activities/join/{id}
        //Join activity and the chat associated with it
        [HttpPost("join/{id}")]
        public async Task<HttpResponseMessage> Join(string id)
        {
            string activityId = id;
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            Console.WriteLine(username);
            Console.WriteLine(password);

            if(activityId != null) {
                try {
                    GetItemResponse stuff = await db.client.GetItemAsync(
                        tableName: "RoostActivities",
                        key: new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
                        {
                            {"ActivityId", new AttributeValue {S = id} }
                        }
                    );

                    Console.WriteLine(stuff.Item["members"].SS);
                    Console.WriteLine(stuff.Item["numMembers"].N);
                    List<string> membersList = stuff.Item["members"].SS;
                    if(membersList.Contains(username)) {
                        Console.WriteLine("User already added to activity");
                        Response.StatusCode = 400;
                        HttpResponseMessage responsey = new HttpResponseMessage();
                        return responsey;
                    }
                    membersList.Add(username);
                    Console.WriteLine(stuff.Item["numMembers"].N);
                    int numberOfPeeps = Convert.ToInt32(stuff.Item["numMembers"].N);
                    numberOfPeeps++;
                    string peopleNum = numberOfPeeps.ToString();

                    await db.client.UpdateItemAsync(
                    tableName: "RoostActivities",
                    key: new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
                    {
                        {"ActivityId", new AttributeValue {S = id} }
                    },

                    attributeUpdates: new Dictionary<string, AttributeValueUpdate>
                    {
                            {"members", new AttributeValueUpdate(new AttributeValue {SS = membersList}, AttributeAction.PUT)},
                            {"numMembers", new AttributeValueUpdate(new AttributeValue {N = peopleNum}, AttributeAction.PUT)}
                    }
                );

                Response.StatusCode = 200;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
                }
                catch(Exception) {
                    Console.WriteLine("caught exception exception");
                    Response.StatusCode = 400;
                    HttpResponseMessage response = new HttpResponseMessage();
                    return response;
                }
            }
            else {
                Response.StatusCode = 400;
                HttpResponseMessage response = new HttpResponseMessage();
                return response;
            }
        }
    }
}
