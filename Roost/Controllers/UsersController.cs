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

namespace Roost.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {

        private readonly IUserRepository _userRepository;
        DBHelper db = new DBHelper();

        public UsersController(IUserRepository userRepository)

        {
            _userRepository = userRepository;
            //Congrats, I'm initialized!
            //TODO:  Initialize DBHelper here bruh
        }
        //[HttpGet]
        //public IActionResult List()
        //{
        //	return Ok(_userRepository.All);
        //}

        //route is localhost:5000/api/users
        //this route actually works...       
        [HttpGet]
        public async Task<String> Get()
        {
            try
            {
                //await db.client.GetItemAsync(
                //tableName: "User",
                //key: new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
                //{	
                //{"userId", new AttributeValue {S = "777"} },
                //{"displayName", new AttributeValue {S = "joe"} },
                //{"password", new AttributeValue {S = "blah"} }
                //}
                return "Soup";
                // );
                //return "User exists";
            }
            catch (Exception)
            {
                return "Error: Incorrect username or password";
            }

            return "rawr";
        }
        // GET: /api/user/login
        [HttpGet("login/{id}/{passHash}")]
        public async Task<HttpResponseMessage> Login(String id, String passHash)
        {
            //this takes request parameters only from the query string
            try
            {
		Console.WriteLine("meow " + id);
		//Console.WriteLine("meow" + qsList.size());
		//System.Diagnostics.Debug.WriteLine("arf");
		//string[] array = new string[queryStrings.Count];
		
		//queryStrings.CopyTo(array, 0);

                GetItemResponse stuff = await db.client.GetItemAsync(
                    tableName: "User",
                    key: new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
                    {
                        {"userId", new AttributeValue {S = id} },
                        {"displayName", new AttributeValue {S = id} },
                        //{"password", new AttributeValue {S = "202cb962ac59075b964b07152b234b70"} }
                    }
                );
		if(stuff.Item["password"].S == passHash) {
			Response.StatusCode = 200;
			HttpResponseMessage response = new HttpResponseMessage();
			return response;
		}
		else {
			Response.StatusCode = 400; 
			HttpResponseMessage response = new HttpResponseMessage();
			return response;

		}		
		//return "meow";
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
		HttpResponseMessage response = new HttpResponseMessage();
		return response;
            }
        }

        // PUT: /api/user/{id}/updateuser
        // Update user info
        [HttpPut("{id}/update")]
        public async void UpdateUser(string id)
        {
            string updatedInfo = Response.Headers["updatedInfo"];
            try
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
                        {"info", new AttributeValueUpdate(new AttributeValue {S = updatedInfo}, AttributeAction.PUT)}
                    }
                );
            }
            catch (Exception)
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
            try
            {
                await db.client.PutItemAsync(
                    tableName: "User",
                    item: new Dictionary<string, AttributeValue>
                    {
                        {"userId", new AttributeValue {S = "777"} },
                        {"displayName", new AttributeValue {S = "joe"} },
                        {"password", new AttributeValue {S = "blah"} }
                    }
                );

                return "User created successfully";
            }
            catch (Exception)
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
                        {
                            "settings", new AttributeValueUpdate(new AttributeValue {S = settings}, AttributeAction.PUT)
                        }
                    }
                );
                return settings;
            }
            catch (Exception)
            {
                return "Error: something went wrong.";
            }

        }

    }
}
