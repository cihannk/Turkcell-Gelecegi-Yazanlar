using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartyApplication.Models;

namespace PartyApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Name = "Cihan";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public IActionResult Register(PartyAttendee partyAttendee)
        {
            ViewBag.JoinSuccess = false;
            if (ModelState.IsValid) {
                if (partyAttendee.Age >= 18)
                    ViewBag.JoinSuccess = true;
            }
            else
            {
                return View("Register");
            }

            return View("Status");
        }

    }
}
