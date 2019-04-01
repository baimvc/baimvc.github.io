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
using System.Reflection;
using System;
using System.Linq;
using BlogDemo.Api.Options;
using System.Collections.Generic;

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Blog API", Version = "v1" });

                //向生成的Swagger添加一个或多个“securityDefinitions”，用于API的登录校验
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Flow = "ResourceOwnerPassword", // 只需通过浏览器获取令牌（适用于swagger）
                    AuthorizationUrl = "http://localhost:6000/connect/authorize",//获取登录授权接口
                    Scopes = new Dictionary<string, string> {
                        { "NewsClient2", "My API News Blog" }//指定客户端请求的api作用域。 如果为空，则客户端无法访问
                    }
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>(); // 添加IdentityServer4认证过滤


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });





            services.AddMvcCore()//mvc并带验证
           .AddAuthorization()
           .AddJsonFormatters();
            //注册IdentityServer
            var identityServerOptions = new IdentityServerOptions();
            Configuration.Bind("IdentityServerOptions", identityServerOptions);
            services.AddAuthentication(identityServerOptions.IdentityScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.RequireHttpsMetadata = false; //是否启用https
                    options.Authority = $"http://{identityServerOptions.ServerIP}:{identityServerOptions.ServerPort}";//配置授权认证的地址
                    options.ApiName = identityServerOptions.ResourceName; //资源名称，跟认证服务中注册的资源列表名称中的apiResource一致
                }
                );


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
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsCommentRepository, NewsCommentRepository>();
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
            propertyMappingContainer.Register<NewsCommentPropertyMapping>();//注册NewsComment要映射的类
            services.AddSingleton<IPropertyMappingContainer>(propertyMappingContainer);//注册到.netcoer单例中

        }
        public void Configure(IApplicationBuilder app)
        {
            //启用Swagger
            app.UseSwagger(o =>
            {
                o.PreSerializeFilters.Add((document, request) =>
                {
                    document.Paths = document.Paths.ToDictionary(p => p.Key.ToLowerInvariant(), p => p.Value);
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API V1");
                c.OAuthClientId("NewsClient2");//客服端名称
                c.OAuthAppName("My API News Blog"); // 描述
                //c.RoutePrefix = "";
            });
        
           
            app.UseDeveloperExceptionPage();
            app.UseAuthentication();//添加验证
            app.UseMvc();
            //app.UseHttpsRedirection(); //启用https

            
        }
    }
}
