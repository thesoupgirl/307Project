using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RoostApp.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        // PUT: /api/user/{id}/updateuser
        // Update user info
        [HttpPut("{id:int}/updateuser")]
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
    }
}
