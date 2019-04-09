using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SQ.Proton.Gateway.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddOcelot(Configuration).AddConsul().AddPolly();

            // Swagger
            services.AddMvc();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc($"{Configuration["Swagger:DocName"]}", new Info
                {
                    Title = Configuration["Swagger:Title"],
                    Version = Configuration["Swagger:Version"]
                });

                //options.OperationFilter<AuthResponsesOperationFilter>();

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //app.UseMvc();
            var apiList = Configuration["Swagger:ServiceDocNames"].Split(',').ToList();
            app.UseMvc()
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    apiList.ForEach(apiItem =>
                    {
                        options.SwaggerEndpoint($"/doc/{apiItem}/swagger.json", apiItem);
                    });
                });


            // Ocelot
            app.UseOcelot().Wait();
        }
    }

    //public class AuthResponsesOperationFilter : IOperationFilter
    //{
    //    public void Apply(Operation operation, OperationFilterContext context)
    //    {
    //        operation.Parameters.Add(new NonBodyParameter()
    //        {
    //            Name = "X-Forwarded-Prefix",
    //            In = "header", //query formData ..
    //            Description = "身份验证票据",
    //            Required = false,
    //            Type = "string",
    //        });
    //    }
    //}
}
