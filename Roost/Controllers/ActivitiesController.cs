using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Amazon.DynamoDBv2.Model;

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

        // POST: /api/activities/add
        [HttpPost("add")]
        public async Task<HttpResponseMessage> AddActivity()
        {
            try
            {

                await db.client.PutItemAsync(
                    tableName: "RoostActivities",
                    item: new Dictionary<String, AttributeValue>
                    {
                        // The unique activity id
                        {"ActivityId", new AttributeValue { S = ""} }, 

                        // The number of members in the group
                        {"NumMembers", new AttributeValue { N = "1"} },

                        // The date the group was created
                        {"CreatedDate", new AttributeValue { S = System.DateTime.Today.ToString()}  }
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
    }
}
