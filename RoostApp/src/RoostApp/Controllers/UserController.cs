using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;

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
        public async void CreateUser()
        {
            await db.client.PutItemAsync(
                tableName: "User",
                item: new Dictionary<string, AttributeValue>
                {
                    {"userId", new AttributeValue {S = ""} },
                    {"displayName", new AttributeValue {S = ""} }
                }
            );
        }

        // POST: /api/user/{id}
        [HttpPost("{id}")]
        // Saves user settings in DB
        public async void SaveSettings(string id)
        {
            await db.client.UpdateItemAsync(
                tableName: "User",
                key: new Dictionary<string, AttributeValue>
                {
                    // Find the user based on their id and display name
                    {"userId", new AttributeValue {S = id} },
                    {"displayName", new AttributeValue {S = "Hello world"} }
                },

                // Choose the attribute(s) we want to update
                attributeUpdates: new Dictionary<string, AttributeValueUpdate>
                {
                    {"settings", new AttributeValueUpdate(new AttributeValue {S = "Distance: 15mi"}, AttributeAction.PUT)}
                }
            );
        }
    }
}
