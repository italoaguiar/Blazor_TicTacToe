using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TesteDTI.Controllers
{
    [Route("/[controller]")]
    public class GameController : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }
    }
}