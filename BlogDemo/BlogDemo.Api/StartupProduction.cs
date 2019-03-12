using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlogDemo.Api
{
    public class StartupProduction
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //注册https中间件
            services.AddHttpsRedirection(options => {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });
            //建议生产环境注册的中间件配置
            services.AddHsts(options => {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("example.com");
                options.ExcludedHosts.Add("www.example.com");

            });
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseHsts();//建议生产环境注册的中间件配置
            app.UseMvc();
            app.UseHttpsRedirection(); //启用https
        }
    }
}
