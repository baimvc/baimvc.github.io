using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BlogDemo.Api
{
    public class StartupDevelopment
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //启用https
            services.AddHttpsRedirection(options => {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });
            services.AddMvc();//启用MVC服务
           
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();//注册https中间件
            app.UseMvc();
        }
    }
}
