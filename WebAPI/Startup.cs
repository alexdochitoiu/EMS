using Business;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WebAPI.Models;
using WebAPI.Seeders;

namespace WebAPI
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
            var dbConnection = Configuration.GetConnectionString("EMSConnection");
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddDbContext<DatabaseService>(options =>
                options.UseSqlServer(dbConnection));

            var config = new AutoMapper.MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                }
            );
            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddTransient<CountriesDbSeeder>();
            services.AddTransient<CitiesDbSeeder>();

            services.AddMvc();

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = ".NET Core EMS API",
                    Description = "ASP.NET Core",
                    TermsOfService = "None",
                    License = new License { Name = "MIT", Url = "https://en.wikipedia.org/wiki/MIT_License" }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            CountriesDbSeeder countriesDbSeeder,
            CitiesDbSeeder citiesDbSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable miWddleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            // Visit http://localhost:5000/swagger
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();

            countriesDbSeeder.Seed();
            citiesDbSeeder.Seed();
        }
    }
}
