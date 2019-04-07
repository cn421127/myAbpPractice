using System;
using System.IO;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.EntityFrameworkCore;
using myAbpBasic.EntityFrameworkCore;
using Castle.Facilities.Logging;
using myAbpBasic.Configuration;
using myAbpBasic.MicroService.Core.Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace myAbpBasic.Web.Startup
{
    public class Startup
    {
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        //public Startup(IConfiguration _appConfiguration)
        //{
        //    _appConfiguration = _appConfiguration;
        //}

        //public IConfiguration _appConfiguration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Configure DbContext
            services.AddAbpDbContext<myAbpBasicDbContext>(options =>
            {
                DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            // Swagger
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc(_appConfiguration["Service:DocName"], new Info
                {
                    Title = _appConfiguration["Service:Title"],
                    Version = _appConfiguration["Service:Version"],
                    Description = _appConfiguration["Service:Description"],
                    Contact = new Contact
                    {
                        Name = _appConfiguration["Service:Contact:Name"],
                        Email = _appConfiguration["Service:Contact:Email"]
                    }
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, _appConfiguration["Service:XmlFile"]);
                s.IncludeXmlComments(xmlPath);
            });


            //Configure Abp and Dependency Injection
            return services.AddAbp<myAbpBasicWebModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            app.UseAbp(); //Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // swagger
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "doc/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint($"/doc/{_appConfiguration["Service:DocName"]}/swagger.json",
                    $"{_appConfiguration["Service:Name"]} {_appConfiguration["Service:Version"]}");
            });

            // register this service to consul
            //app.RegisterConsul(lifetime, new ServiceEntity(_appConfiguration));
        }
        
    }

    public static class HostingEnvironmentExtensions
    {
        public static IConfigurationRoot GetAppConfiguration(this IHostingEnvironment env)
        {
            return AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }
    }
}
