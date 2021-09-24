using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using WorkPlaces.Common.Constants;
using WorkPlaces.Data;
using WorkPlaces.Data.Interfaces;
using WorkPlaces.Data.Repositories;
using WorkPlaces.Service.Interfaces;
using WorkPlaces.Service.Services;

namespace WorkPlaces
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
            services.AddCors();
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    return new BadRequestObjectResult(errors);
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(GlobalConstants.SwaggerVersion, new OpenApiInfo
                {
                    Title = GlobalConstants.SwaggerTitle,
                    Version = GlobalConstants.SwaggerVersion
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, GlobalConstants.XMLDocument);
                c.IncludeXmlComments(filePath);
            });

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IWorkPlacesRepository, WorkPlacesRepository>();
            services.AddScoped<IUserWorkplacesRepository, UserWorkPlacesRepository>();

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IWorkPlacesService, WorkPlacesService>();
            services.AddTransient<IUserWorkPlacesService, UserWorkPlacesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Work Places API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
