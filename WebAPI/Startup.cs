using Data.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Infrastructure;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            IocContainer.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddDbContext();

            services.AddTrasitentServices();

            services.AddAutoMapper();

            services.AddUnitOfWork();

            services.SetRegisterPolicy();

            services.AddJwtAuthentication();

            services.AddCors();

            services.AddMvc();

            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IDatabaseSeeder dbSeeder)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors(corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyOrigin();
                corsPolicyBuilder.AllowAnyMethod();
                corsPolicyBuilder.AllowAnyHeader();
                corsPolicyBuilder.WithExposedHeaders("X-InlineCount");
            });

            var records = dbSeeder.SeedAsync().Result;

            app.UseMvc();
        }
    }
}
