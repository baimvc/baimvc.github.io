using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Web.ApplicationCore.Entities;
using BlogWeb.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        [ValidateAntiForgeryToken]//防止跨站攻击
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("登录成功！登录名为：{userName}", model.UserName);
                return RedirectToLocal(returnUrl);

            }
            else
            {
                _logger.LogWarning("登录失败！登录名为{userName}",model.UserName);
                ModelState.AddModelError("","用户名或密码错误");
                return View(model);
            }

        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName,Email = model.Email,CreateOn = DateTime.Now};
                var result = await _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("创建用户成功！用户名为：{userName}",model.Email);
                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail","Account",
                        new { userId = user.Id,code = code},
                        protocol:HttpContext.Request.Scheme);
                    await MessageServices.SendEmailAsync(model.Email, "确认您的帐户", "请单击此链接确认您的帐户：<a href = \"" + callbackUrl + "\">link</a>");
                    if (user.UserName.ToLower().Equals("admin"))
                    {
                        await _userManager.AddClaimAsync(user, new Claim("Admin","Allowed"));
                    }
                    return RedirectToAction("Login");
                }
                AddErrors(result);

            }
            return View(model);
        }
        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOff()
        {
            var userName = HttpContext.User.Identity.Name;
            await _signInManager.SignOutAsync();
            _logger.LogInformation("{userName}退出登录。",userName);
            return RedirectToAction("Index","Home");
        }


        #region 辅助方法
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
                _logger.LogWarning("创建用户时出错： {error}", error.Description);
            }
        }

        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
       
    }
}
