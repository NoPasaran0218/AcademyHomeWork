using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MatOrderingService.Service.Storage;
using MatOrderingService.Service.Storage.Impl;
using AutoMapper;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using MatOrderingService.Service.Auth;
using Microsoft.AspNetCore.Authorization;
using MatOrderingService.Service.Swagger;
using MatOrderingService.Filters;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.AspNetCore.Razor.CodeGenerators;
using MatOrderingService.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MatOrderingService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().
                SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<EnvironmentInfo>(Configuration.GetSection("EnvironmentInfo"));
            services.Configure<CodeGeneratorOptions>(Configuration.GetSection("CodeGenerator"));
            services.AddMvc(/*options=> 
            {
                options.Filters.Add(typeof(ExceptionFilter));
            }*/);
            services.AddSingleton<IOrderList, OrderList>();
            services.AddSingleton<IProductList, ProductList>();
            services.AddSingleton<ICodeGeneratorService,CodeGeneratorService>();
            var connectionString = Configuration.GetValue<string>("Data:ConnectionString");
            services.AddDbContext<OrdersDbContext>(options => options.UseSqlServer(connectionString).ConfigureWarnings(warning=>warning.Log(RelationalEventId.QueryClientEvaluationWarning)));
            services.AddScoped<IOrdersDbContext>(context=>context.GetService<OrdersDbContext>());
            services.AddAutoMapper();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Materialise Academy Orders API", Version = "v1" });
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "MatOrderingService.xml");
                c.IncludeXmlComments(filePath);
                c.OperationFilter<SwaggerAuthorizationHeaderParameter>(Configuration.GetValue<string>("AuthOptions:AuthenticationScheme"));
            });
            services.Configure<MatOsAuthOptions>(Configuration.GetSection("AuthOptions"));
            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(Configuration.GetValue<string>("AuthOptions:AuthenticationScheme"))
                .RequireAuthenticatedUser().Build();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            app.UseMiddleware<MatOsAuthMiddleware>();
            app.UseMiddleware<CustomMiddleware>();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MatOrderingService");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Mat Prefix: ");
                await next.Invoke();
            });

            app.Map("/info", Info);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(Configuration["EnvironmentInfo:WelcomeMessage"]);
            });
        }

        private static void Info(IApplicationBuilder app)
        {
           app.Map("/environment", EnvironmentInfo);
           app.Run(async context =>
            {
                await context.Response.WriteAsync("Deep Info");
            });
        }

        private static void EnvironmentInfo(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Environment2017");
            });
        }
    }
}
