using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2;

namespace RoostApp.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        DBHelper db = new DBHelper();

        // GET: /api/user/login
        [HttpGet("login/{id}/{password}")]
        public async Task<string> Login(string id, string password)
        {
            string login = Response.Headers["loginInfo"];

            var loginJson = Document.FromJson(login);

            try
            {
                await db.client.GetItemAsync(
                    tableName: "User",
                    key: new Dictionary<string, AttributeValue>
                    {
                        {"userId", new AttributeValue {S = id} },
                        {"displayName", new AttributeValue {S = "joe"} },
                        //{"password", new AttributeValue {S = "blah"} }
                    }
                );
                return "User exists";
            } catch (Exception)
            {
                return "Error: Incorrect username or password";
            }
        }

        // PUT: /api/user/{id}/updateuser
        // Update user info
        [HttpPut("{id}/update")]
        public async void UpdateUser(string id)
        {
            string updatedInfo = Response.Headers["updatedInfo"];

            var infoJson = Document.FromJson("updatedInfo");

            try
            {
                await db.client.UpdateItemAsync(
                    tableName: "User",
                    key: new Dictionary<string, AttributeValue>
                    {
                        // Find the user based on their id and display name
                        {"userId", new AttributeValue {N = "1"} },
                        {"displayName", new AttributeValue {S = infoJson["displayName"]} }
                    },

                    attributeUpdates: new Dictionary<string, AttributeValueUpdate>
                    {
                        {"info", new AttributeValueUpdate(new AttributeValue {S = updatedInfo}, AttributeAction.PUT)}
                    }
                );
            } catch (Exception)
            {

            }
        }

        // POST: /api/user/create
        [HttpPost("create")]
        [HttpGet("create")]
        // Create a new user
        public async Task<string> CreateUser()
        {
            string info = Response.Headers["userInfo"];

            var infoJson = Document.FromJson(info);

            int id = 0;

            try
            {
                var newUser = await db.client.PutItemAsync(
                    tableName: "User",
                    item: new Dictionary<string, AttributeValue>
                    {
                        {"userId", new AttributeValue {S = infoJson["displayName"]} },
                        {"displayName", new AttributeValue {S = infoJson["displayName"]} },
                        {"password", new AttributeValue {S = infoJson["password"]} }
                    }
                );

                return "User created successfully";
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
            // The settings will be stored as JSON in the response header.
            string settings = Response.Headers["settings"];

            var sJson = Document.FromJson(settings);

            try
            {
                await db.client.UpdateItemAsync(
                    tableName: "User",
                    key: new Dictionary<string, AttributeValue>
                    {
                        {"userId", new AttributeValue {S = id} },
                        {"displayName", new AttributeValue {S = "Hello world"} }
                    },

                    attributeUpdates: new Dictionary<string, AttributeValueUpdate>
                    {
                        // Change username and password (string), push notifications (boolean), distance (int)
                        {
                            "displayName",
                            new AttributeValueUpdate(new AttributeValue {S = sJson["newUsername"]}, AttributeAction.PUT)
                        },
                        {
                            "password",
                            new AttributeValueUpdate(new AttributeValue {S = sJson["password"]}, AttributeAction.PUT)
                        },
                        {
                            "notifications",
                            new AttributeValueUpdate(new AttributeValue {BOOL = sJson["notifications"].AsBoolean()}, AttributeAction.PUT)
                        },
                        {
                            "distance",
                            new AttributeValueUpdate(new AttributeValue {N = sJson["distance"]}, AttributeAction.PUT)
                        }
                    }
                );
                return settings;
            } catch (Exception)
            {
                return "Error: something went wrong.";
            }
            
        }
    }
}
