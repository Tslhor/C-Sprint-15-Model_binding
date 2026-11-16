using System;
using Microsoft.AspNetCore.Mvc;
using ProductsValidation.Models;
using ProductsValidation.Services;

namespace ProductsValidation.Controllers
{
    public class UsersController : Controller
    {
        private const string NotExistsViewPath = "~/Views/Shared/NotExists.cshtml";
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        
        public IActionResult Index(string id)
        {
            return View("Index", userService.GetUsers());
        }

        [HttpGet]
        public IActionResult Create([FromQuery] User user)
        {
            return View(user);
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost([FromQuery] User user)
        {
            userService.AddUser(user);

            return RedirectToAction(nameof(View), new { id = user.Id });
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            var user = userService.GetUser(id);
            if (user == null)
            {
                return View(NotExistsViewPath);
            }

            return View(user);
        }
    }
}
