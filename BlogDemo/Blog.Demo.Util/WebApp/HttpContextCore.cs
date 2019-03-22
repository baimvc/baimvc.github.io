using Microsoft.AspNetCore.Http;

namespace Blog.Demo.Util
{
    public static class HttpContextCore
    {
        public static HttpContext Current { get => AutofacHelper.GetService<IHttpContextAccessor>().HttpContext; }
    }
}
