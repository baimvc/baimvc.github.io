using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogDemo.Web.Areas.Admin.Query;
using Microsoft.AspNetCore.Mvc;



namespace BlogDemo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Login()
        {
            return View();
        }
      
        [HttpPost]
        public IActionResult Login(UserLoginQuery userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.VCode))
            {
                Session["VCode"] = string.Empty;
                return Error(message: "请正确输入验证码");
            }

            var sessionCode = Session["VCode"].ToString();
            if (string.IsNullOrEmpty(sessionCode) || sessionCode.ToUpper() != uModel.VCode.ToUpper())
            {
                Session["VCode"] = string.Empty;
                return Error(message: "验证码信息不正确");
            }
            if (_sysUser.LoginChecked(userLogin))
            {
                var user = _sysUser.GetUserList(uModel);
                Session["UserName"] = user.uLoginName;
                Session[Key.LoginInfo] = user;
            }
            else
            {
                Session["VCode"] = string.Empty;
                return Error(message: "用户名密码不正确");
            }
            return Success();


        }
        [HttpGet]
        public IActionResult ValidateCode()
        {

            string oldcode = Session["VCode"] as string;
            string code = ValidateCodeHelper.CreateRandomCode(4); //验证码的字符为4个
            Session["VCode"] = code; //验证码存放在TempData中
            return File(ValidateCodeHelper.CreateValidateGraphic(code), "image/Jpeg");
        }
    }
}
