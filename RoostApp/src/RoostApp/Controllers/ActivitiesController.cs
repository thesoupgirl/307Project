using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RoostApp.Controllers
{
    [Route("api/activities")]
    public class ActivitiesController : Controller
    {
        // GET: /api/activities/{id}/{dist}
        // Gets list of activities within certain radius of user
        [HttpGet("{id:int}/{dist}")]
        public IActionResult FindActivities()
        {
            return View();
        }

        // GET: /api/activities/category/{id}/{dist}
        // Gets list of activities within certain radius of user by category
        [HttpGet("{id:int}/{dist}")]
        public IActionResult FindActivitiesByCategory()
        {
            return View();
        }

        // POST: /api/activities/{id}/createactivity
        // Creates an activity
        [HttpPost("{id:int}/createactivity")]
        public IActionResult CreateActivity()
        {
            return View();
        }

        // POST: /api/activities/{id}/deleteactivity
        // Deletes an activity
        [HttpPost("{id:int}/deleteactivity")]
        public IActionResult DeleteActivity()
        {
            return View();
        }
    }
}
