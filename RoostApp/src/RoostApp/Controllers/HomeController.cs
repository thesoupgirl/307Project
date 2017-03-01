using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RoostApp.Controllers
{
    [Route("api/")]
    public class HomeController : Controller
    {
        DBHelper db = new DBHelper();

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("login")]
        public async void Login()
        {
            var response = db.client.GetItemAsync(
                tableName: "User",
                key: new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
                {
                    // Find the user based on their display name and password
                }
            );
        }
    }
}
