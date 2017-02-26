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
        [HttpPut("{id}/updateuser")]
        public IActionResult UpdateUser()
        {
            return View();
        }

        // POST: /api/user/create
        [HttpPost("create")]
        // Create a new user
        public IActionResult CreateUser()
        {
            return View();
        }

        // POST: /api/user/{id}/settings/update
        [HttpPost("{id}/savesettings")]
        // Saves user settings in DB
        public async void SaveSettings()
        {
            await db.client.PutItemAsync(
                tableName: "User",
                item: new Dictionary<string, AttributeValue>
                {
                    // Save the settings as a string
                    {"UserId", new AttributeValue {S = "{id}"} },
                    {"Settings", new AttributeValue {S = ""} }
                }
            );
        }
    }
}
