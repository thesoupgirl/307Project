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
        public async Task<HttpResponseMessage> CreateActivity(string id)
        {
            try
            {
                await db.client.PutItemAsync(
                    tableName: "RoostActivities",
                    item: new Dictionary<String, AttributeValue>
                    {
                        // The unique activity id
                        {"ActivityId", new AttributeValue { S = id} }, 

                        // The number of members in the group
                        {"numMembers", new AttributeValue { N = "1"} },

                        // The date the group was created
                        {"createdDate", new AttributeValue { S = System.DateTime.Today.ToString()}  },

                        // The categories the activity will be listed under
                        {"categories", new AttributeValue {SS = new List<string>() } },

                        // The name of the group
                        {"name", new AttributeValue { S = "" } },

                        // The description of the group
                        {"description", new AttributeValue { S = "" } },

                        // The unique ID of the chat assiciated with this activity
                        {"chatId", new AttributeValue { S = "" } },

                        // The identifier of whether the chat is public (open) or private (closed)
                        {"status", new AttributeValue { S = "open"} },

                        // The maximum amount of people who can join the group
                        {"maxGroupSize", new AttributeValue{ N = "20"} },

                        // The userId of the person who created the group
                        {"groupLeader", new AttributeValue{ S = ""} }
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
