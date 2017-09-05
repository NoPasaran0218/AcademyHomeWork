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
using asp.netCore.Middleware;
using asp.netCore.BL;
using Microsoft.AspNetCore.Routing;

namespace asp.netCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<OptionsInfo>(Configuration.GetSection("EnvironmentInfo"));
            services.AddMvc();
            services.AddSingleton<ICodeGenerator>(generator=>new CodeGenerator(Configuration["EnvironmentInfo:CodePrefix"]));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Materialise Academy Orders API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<MyMiddleware>();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Materialise Academy Orders Api");
            });
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Map("/info", (ap) =>
            {
                ap.Run(async context =>
                {
                    await context.Response.WriteAsync(Configuration["EnvironmentInfo:CodePrefix"]);
                });
            });
            //app.Map("/code/{id}", GetCode);
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet("api/code/{id:int}", context =>
            {

            });
            routeBuilder.Build();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
            
        }
        public static void GetCode(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(context.Response.HttpContext.Items["prefix"].ToString()+CodeGenerator.GetCodeStatic((int)context.Items["id"]));
            });
        }
    }
}
