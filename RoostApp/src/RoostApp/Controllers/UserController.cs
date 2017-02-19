using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoostApp.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        // PUT: /api/user/{id}/updateuser
        // Update user info
        [HttpPut("{id:int}/updateuser")]
        public IActionResult updateUser()
        {
            return View();
        }

        // POST: /api/user/create
        [HttpPost("create")]
        public IActionResult createUser()
        {
            return View();
        }
    }
}
