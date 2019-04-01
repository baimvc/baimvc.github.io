using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace QuickstartIdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //注册IdentityServer
            services.AddIdentityServer()
              .AddDeveloperSigningCredential()
              .AddInMemoryApiResources(ApiConfig.GetApiResources())//添加api资源
              .AddInMemoryClients(ApiConfig.GetClients())//添加客户端
              .AddTestUsers(ApiConfig.GetUsers())//添加测试用户
              .AddInMemoryIdentityResources(ApiConfig.GetIdentityResources());//添加对OpenID Connect的支持
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}
