using BlogDemo.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using BlogDemo.Core.Interface;
using BlogDemo.Infrastructure.Imp;
using AutoMapper;
using BlogDemo.Infrastructure.Resources;
using FluentValidation;

namespace BlogDemo.Api
{
    public class StartupDevelopment
    {
       public static IConfiguration Configuration { get; set; }
        public StartupDevelopment(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

          
            services.AddMvc();//启用MVC服务
            //var connectionString = Configuration["ConnectionStrings:DefaultConnection"];//第一种方式获取appsettings配置文件中的值。

            var connectionString = Configuration.GetConnectionString("DefaultConnection");  
            services.AddDbContext<MyDBContext>(options => options.UseSqlServer(connectionString));
            //注册https中间件
            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //    options.HttpsPort = 5001;
            //});
            //注册实例
            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //注册AutoMapper
            services.AddAutoMapper();
            //注册验证工具（FluentValidation）
            services.AddTransient<IValidator<BannerResources>, BannerResourcesValidator>();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
            //app.UseHttpsRedirection(); //启用https
        }
    }
}
