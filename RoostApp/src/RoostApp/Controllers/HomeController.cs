using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RoostApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /api/gettoken
        // GET: /api/activities/{id}/{dist}
        // GET: /api/activities/category/{id}/{dist}
        // GET: /api/chat/{id}/{chat}/messages
        // GET: /api/chat/{id}/threads
    }
}
