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

        // GET: /api/user/login
        [HttpGet("login")]
        public async void Login()
        {
            var response = await db.client.GetItemAsync(
                tableName: "User",
                key: new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
                {
                    // Find the user based on their display name and password
                    // and set a bool to true
                }
            );
        }

        // PUT: /api/user/{id}/updateuser
        // Update user info
        [HttpPut("{id}/update")]
        public async void UpdateUser(string id)
        {
            await db.client.UpdateItemAsync(
                tableName: "User",
                key: new Dictionary<string, AttributeValue>
                {
                    // Find the user based on their id and display name
                    {"userId", new AttributeValue {S = id} },
                    {"displayName", new AttributeValue {S = "Hello world"} }
                },

                attributeUpdates: new Dictionary<string, AttributeValueUpdate>
                {
                    {"info", new AttributeValueUpdate(new AttributeValue {S = ""}, AttributeAction.PUT)}
                }
);
        }

        // POST: /api/user/create
        [HttpPost("create")]
        [HttpGet("create")]
        // Create a new user
        public async Task<string> CreateUser()
        {
            try
            {
                var response = await db.client.GetItemAsync(
                    tableName: "User",
                    key: new Dictionary<string, AttributeValue>
                    {
                        {"userId", new AttributeValue {S = "777"} },
                        {"displayName", new AttributeValue {S = "joe"} }
                    }
                );

                return response.Item["userId"].S;
            } catch (Exception)
            {
                return "User not found.";
            }
        }

        // POST: /api/user/{id}
        [HttpPost("{id}")]
        [HttpGet("{id}")]
        // Saves user settings in DB
        public async Task<string> SaveSettings(string id)
        {
            string settings = Response.Body.ToString();

            try
            {
                var response = await db.client.UpdateItemAsync(
                    tableName: "User",
                    key: new Dictionary<string, AttributeValue>
                    {
                        {"userId", new AttributeValue {S = id} },
                        {"displayName", new AttributeValue {S = "Hello world"} }
                    },

                    attributeUpdates: new Dictionary<string, AttributeValueUpdate>
                    {
                        {
                            "settings", new AttributeValueUpdate(new AttributeValue {S = settings}, AttributeAction.PUT)
                        }
                    }
                );
                return "Settings have been updated.";
            } catch (Exception)
            {
                return "Error: something went wrong.";
            }
            
        }
    }
}
