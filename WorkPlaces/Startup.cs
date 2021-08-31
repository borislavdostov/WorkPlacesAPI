using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IWorkPlacesRepository, WorkPlacesRepository>();
            services.AddScoped<IUserWorkPlacesRepository, UserWorkPlacesRepository>();

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IWorkPlacesService, WorkPlacesService>();
            services.AddTransient<IUserWorkPlacesService, UserWorkPlacesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
