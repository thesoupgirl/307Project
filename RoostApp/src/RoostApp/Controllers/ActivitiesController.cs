using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoostApp.Controllers
{
    [Route("api/activities")]
    public class ActivitiesController : Controller
    {
        // GET: /api/activities/{id}/{dist}
        // Gets list of activities within certain radius of user
        [HttpGet("{id:int}/{dist}")]
        public IActionResult findActivities()
        {
            return View();
        }

        // GET: /api/activities/category/{id}/{dist}
        // Gets list of activities within certain radius of user by category
        [HttpGet("{id:int}/{dist}")]
        public IActionResult findActivitiesByCategory()
        {
            return View();
        }

        // POST: /api/activities/{id}/createactivity
        // Creates an activity
        [HttpPost("{id:int}/createactivity")]
        public IActionResult createActivity()
        {
            return View();
        }

        // POST: /api/activities/{id}/deleteactivity
        // Deletes an activity
        [HttpPost("{id:int}/deleteactivity")]
        public IActionResult deleteActivity()
        {
            return View();
        }
    }
}
