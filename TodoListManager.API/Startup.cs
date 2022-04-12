using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using DataLayer;
using BusinessLogic;
using Models.Config;

namespace TodoListManager.API
{
    public class Startup
    {
        private readonly string _allowLocalOrigins = "AllowLocalOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDbRepository, DbRepository>();
            services.AddScoped<ITodoListManager, BusinessLogic.TodoListManager>();
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(name: _allowLocalOrigins,
                    policy =>
                    {
                        policy.AllowAnyHeader()
                              .AllowAnyMethod()
                              .WithOrigins("http://localhost:3000");
                    });
            });

            var connectionStrings = new ConnectionStrings();
            Configuration.Bind("ConnectionStrings", connectionStrings);
            services.AddSingleton(connectionStrings);


            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple ASP.NET Core Web API to store a user's todo list.",
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "Documentation",
                    //    Email = string.Empty,
                    //    Url = new Uri("https://abbott.sharepoint.com/teams/US-ARDx-SCRit/dev/_layouts/15/Doc.aspx?sourcedoc={2389408b-2d7b-458a-9984-ad790ad78bed}&action=edit&wd=target%28How-Tos.one%7C29897118-af4f-4473-acf7-074198eac632%2FUse%20the%20On-Prem%20Key%20Vault%20in%20your%20Application%7Cf4a3c6f4-30c5-4ce9-b4b9-6def35d43be8%2F%29"),
                    //},
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /**
             * Thanks to the following article for
             * helping me set up swagger.
             * https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio
             */
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            /**
             * Thanks to the following article for helping me
             * set up disabling cors policies locally.
             * 
             * https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-3.1
             */
            if (env.IsDevelopment())
            {
                app.UseCors(_allowLocalOrigins);
            }
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
