using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.Model;

namespace RoostApp.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {

        DBHelper db = new DBHelper();

        // PUT: /api/user/{id}/updateuser
        // Update user info
        [HttpPut("{id}/update")]
        public string UpdateUser(string id)
        {
            return "User is updated here";
        }

        // POST: /api/user/create
        [HttpPost("create")]
        // Create a new user
        public string CreateUser()
        {
            return "Create user here.";
        }

        // POST: /api/user/{id}
        [HttpPost("{id}")]
        // Saves user settings in DB
        public async void SaveSettings(string id)
        {
            await db.client.PutItemAsync(
                tableName: "User",
                item: new Dictionary<string, AttributeValue>
                {
                    // Save the settings as a string
                    // Settings will be received as JSON
                    {"userId", new AttributeValue {S = id} },
                    {"displayName", new AttributeValue {S = "Hello world"} },
                    {"settings", new AttributeValue {S = "Distance: 10mi"} }
                }
            );

           
        }
    }
}
