using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogDemo.Infrastructure.Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BlogDemo.Api.Controllers
{
    [Route("api/Home")]
    public class HomeController : ControllerBase
    {
       
        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Ok();
        }
       

    }
}