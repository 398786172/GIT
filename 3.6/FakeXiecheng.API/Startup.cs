using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeXiecheng.API.Database;
using FakeXiecheng.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace FakeXiecheng.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers
                    (setupAction => { setupAction.ReturnHttpNotAcceptable = true; })
                .AddNewtonsoftJson(setupAction =>
                    {
                        setupAction.SerializerSettings.ContractResolver =
                            new CamelCasePropertyNamesContractResolver();
                    })
                    .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setupAction =>
                {
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetail = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "无所谓",
                            Title = "数据验证失败",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "请看详细说明",
                            Instance = context.HttpContext.Request.Path
                        };
                        problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                        return new UnprocessableEntityObjectResult(problemDetail)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                    services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();
                    //services.AddSingleton
                    //services.AddScoped
                    services.AddDbContext<AppDbContext>(options => {
                        options.UseSqlServer(Configuration["DbContext:ConnectionString"]);
                    });
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/test", async context =>
                //{
                //    throw new Exception("test");
                //    //await context.Response.WriteAsync("Hello from test!");
                //});

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});

                endpoints.MapControllers();
            });
        }
    }
}