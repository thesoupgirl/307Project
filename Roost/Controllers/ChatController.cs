using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RoostApp.Controllers
{
    [Route("api/chat")]
    public class ChatController : Controller
    {
        // GET: /api/chat/{id}/{chat}/messages
        [HttpGet("{id}/{chat}/messages")]
        // Gets messages for a thread
        public IActionResult GetMessages(string id, string chat)
        {
            return View();
        }

        // POST: /api/chat/{id}/send
        [HttpPost("{id}/send")]
        // Sends message to the group
        public IActionResult SendMessage(string id)
        {
            return View();
        }

        // GET: /api/chat/{id}/threads
        [HttpGet("{id}/threads")]
        // Gets a list of the threads a user is subscribed to
        public IActionResult GetThreads(string id)
        {
            return View();
        }
    }
}
