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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using BlogDemo.Infrastructure.Services;

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
            //注册Swagger
            //services.AddSwaggerGen(c => {
            //    c.SwaggerDoc("v1", new Info { Title = "DemoAPI", Version = "v1"});
            //    //添加xml文件
            //    c.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), "Api.xml"));
            //});

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
            services.AddScoped<INewsClassifyRepository, NewsClassifyRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //注册AutoMapper
            services.AddAutoMapper();
            //注册验证工具（FluentValidation）
            services.AddTransient<IValidator<BannerResources>, BannerResourcesValidator>();
            //注册Url分页
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });
            //注册 EF查询 根据字段动态排序
            var propertyMappingContainer = new PropertyMappingContainer();//创建属性映射容器
            propertyMappingContainer.Register<BannerPropertyMapping>();//注册Banner要映射的类
            propertyMappingContainer.Register<NewsClassifyPropertyMapping>();//注册NewsClassify要映射的类
            propertyMappingContainer.Register<NewsPropertyMapping>();//注册News要映射的类
            services.AddSingleton<IPropertyMappingContainer>(propertyMappingContainer);//注册到.netcoer单例中

        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
            //app.UseHttpsRedirection(); //启用https

            //启用Swagger
            //app.UseSwagger();
            //app.UseSwaggerUI(c=> {
            //    c.SwaggerEndpoint("swagger/v1/swagger.json","DemoAPI V1");
            //    //加载汉化的js文件，注意 swagger.js文件属性必须设置为“嵌入的资源”。
            //    c.InjectJavascript("/Scripts/swagger.js");
            //});
        }
    }
}
