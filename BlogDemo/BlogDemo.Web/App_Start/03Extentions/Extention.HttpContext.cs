using Blog.Demo.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlogDemo.Web
{
    public static partial class Extention
    {
        public static string MapPath(this HttpContext httpContext, string virtualPath)
        {
            UrlHelper urlHelper = new UrlHelper(AutofacHelper.GetService<IActionContextAccessor>().ActionContext);
            virtualPath = urlHelper.Content(virtualPath);
            return $"{Path.Combine(new List<string> { GlobalSwitch.WebRootPath}.Concat(virtualPath.Split('/')).ToArray())}";
        }

    }
}
