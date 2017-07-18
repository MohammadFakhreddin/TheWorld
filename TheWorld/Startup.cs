using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.models;
using TheWorld.viewModels;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TheWorld
{
    public class Startup
    {
        private IHostingEnvironment env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            this.env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            if (env.IsDevelopment())
            {
                services.AddScoped<IMailService, DebugMailService>();//Making it support our services
            }
            else
            {
                //TODO Implement a real server
            }


            //We use scoped cause we want it's life cycle to be per request
            services.AddScoped<IWorldRepository, WorldRepository>();//For testing we can use Imock repository

            services.AddTransient<GeoCoordsService>();

            services.AddTransient<WorldContextSeedData>();

            services.AddLogging();

            services.AddMvc(config =>
                {
                    if (!env.IsDevelopment())
                    {
                        config.Filters.Add(new RequireHttpsAttribute());
                    }
                }
            ).AddJsonOptions(config =>
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }
            );

            services.AddIdentity<WorldUser, IdentityRole>(config=> {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                config.Cookies.ApplicationCookie.LoginPath = "/auth/login";
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                        ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        await Task.Yield();
                    }
                };
            }).AddEntityFrameworkStores<WorldContext>();

            services.AddEntityFramework()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<WorldContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory,WorldContextSeedData seeder,ILoggerFactory factory)
        {
            loggerFactory.AddConsole();

            AutoMapper.Mapper.Initialize(config => {
                config.CreateMap<TripViewModel, Trip>().ReverseMap();
                config.CreateMap<StopViewModel, Stop>().ReverseMap();
            });

            if (env.IsDevelopment())//Show internal exception only in debug version for only programmer
            {
                app.UseDeveloperExceptionPage();
                factory.AddDebug(LogLevel.Information);
            }
            else
            {
                factory.AddDebug(LogLevel.Error);
            }
            //app.UseDefaultFiles();
 
            //Order matters first static files second use identity and mvc must be after all
            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(config=> {
                config.MapRoute(name: "Default", template: "{controller}/{action}/{id?}",defaults:new { controller = "App", action = "Index"});
            });

            seeder.ensureSeedData().Wait();
        }
    }
}
