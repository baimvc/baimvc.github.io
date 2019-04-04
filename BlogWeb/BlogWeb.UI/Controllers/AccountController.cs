using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Web.ApplicationCore.Entities;
using BlogWeb.UI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogWeb.UI.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<User> _userManager { get; }

        public SignInManager<User> _signInManager { get; }
        private readonly ILogger<AccountController> _logger;
        public AccountController(
          UserManager<User> userManager,
          SignInManager<User> signInManager,
          ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           
        }
    }
}
