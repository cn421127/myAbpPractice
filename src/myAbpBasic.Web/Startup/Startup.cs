using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.EntityFrameworkCore;
using Abp.Extensions;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myAbpBasic.Configuration;
using myAbpBasic.EntityFrameworkCore;
using myAbpBasic.MicroService.Core.Consul;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;

namespace myAbpBasic.Web.Startup
{
    public class Startup
    {
        //public readonly IConfigurationRoot _appConfiguration;

        //public Startup(IHostingEnvironment env)
        //{
        //    _appConfiguration = env.GetAppConfiguration();
        //}

        public Startup(IConfiguration Configuration)
        {
            _appConfiguration = Configuration;
            Logger = NullLogger.Instance;
        }

        public IConfiguration _appConfiguration { get; }
        public ILogger Logger { get; set; }

        private static string postfix = "-gateway";

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


            // identity server
            services.AddMvcCore().AddAuthorization().AddJsonFormatters();
            services.AddAuthentication(_appConfiguration["IdentityService:DefaultScheme"]).AddIdentityServerAuthentication(_appConfiguration["IdentityService:DefaultScheme"], options =>
            {
                options.Authority = _appConfiguration["IdentityService:Uri"];
                options.RequireHttpsMetadata = false;
                options.ApiName = "serviceorder"; // match with configuration in IdentityServer
            });
            //services.AddAuthentication(_appConfiguration["IdentityService:DefaultScheme"])
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = _appConfiguration["IdentityService:Uri"];
            //        options.RequireHttpsMetadata = Convert.ToBoolean(_appConfiguration["IdentityService:UseHttps"]);
            //    });

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
                s.SwaggerDoc(_appConfiguration["Service:DocName"] + postfix, new Info
                {
                    Title = _appConfiguration["Service:Title"] + postfix,
                    Version = _appConfiguration["Service:Version"] + postfix,
                    Description = _appConfiguration["Service:Description"] + postfix,
                    Contact = new Contact
                    {
                        Name = _appConfiguration["Service:Contact:Name"],
                        Email = _appConfiguration["Service:Contact:Email"]
                    }
                });
                s.DocInclusionPredicate((docName, description) =>
                {
                    string actualName = docName.RemovePostFix(postfix);
                    if (docName.Contains(postfix))
                    {
                        if (!description.RelativePath.Contains(actualName))
                        {
                            var values = description.RelativePath
                                .Split('/')
                                .Select(v => v.Replace("api", "api/" + docName.RemovePostFix((postfix))));

                            description.RelativePath = string.Join("/", values);
                        }
                    }
                    else
                    {
                        var values = description.RelativePath
                            .Split('/').ToList();
                        values.RemoveAll((d => d.Contains(actualName)));

                        description.RelativePath = string.Join("/", values);
                    }


                    return true;

                });

                // Define the BearerAuth scheme that's in use
                //s.AddSecurityDefinition("bearerAuth", new ApiKeyScheme()
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //    Name = "Authorization",
                //    In = "header",
                //    Type = "apiKey"
                //});
                //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //var xmlPath = Path.Combine(basePath, _appConfiguration["Service:XmlFile"]);
                //s.IncludeXmlComments(xmlPath);
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

            app.UseAuthentication();
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
                //s.SwaggerEndpoint($"/doc/{_appConfiguration["Service:DocName"]}{postfix}/swagger.json",
                //    $"{_appConfiguration["Service:Name"]} {_appConfiguration["Service:Version"]} {postfix}");
            });

            // register this service to consul
            app.RegisterConsul(lifetime, new ServiceEntity(_appConfiguration));
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
