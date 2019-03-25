using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDemo.Web
{
    public static partial class Extention
    {
        public static string Scrpit(this IUrlHelper urlHelper, string scriptVirtualPath)
        {
            string filePath = urlHelper.ActionContext.HttpContext.MapPath(scriptVirtualPath);
            FileInfo fileInfo = new FileInfo(filePath);
            var lastTime = fileInfo.LastWriteTime.GetHashCode();
            return urlHelper.Content($"{scriptVirtualPath}?_v={lastTime}");
        }
    }
}
