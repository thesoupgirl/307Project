﻿using System;
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
