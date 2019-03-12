using BlogDemo.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using BlogDemo.Core.Interface;
using BlogDemo.Infrastructure.Imp;

namespace BlogDemo.Api
{
    public class StartupDevelopment
    {
       
     
        public void ConfigureServices(IServiceCollection services)
        {

          
            services.AddMvc();//启用MVC服务
            services.AddDbContext<MyDBContext>(options => options.UseSqlServer("Data Source=.;Initial Catalog=DBNews;User ID=sa;Password=123"));
            //注册https中间件
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });
            //注册实例
            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
            app.UseHttpsRedirection(); //启用https
        }
    }
}
