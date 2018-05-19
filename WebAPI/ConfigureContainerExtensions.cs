using Business;
using Data.Core.Domain.Entities;
using Data.Core.Interfaces;
using Data.Persistence;
using Data.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using WebAPI.Mappings;
using WebAPI.Seeders;

namespace WebAPI
{
    public static class ConfigureContainerExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration, string keyConnectionString)
        {
            services.AddTransient<IDatabaseContext, DatabaseContext>();

            var dbConnection = configuration.GetConnectionString(keyConnectionString);
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(dbConnection));

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlite(dbConnection));

            services.AddIdentity<User, UserRole>()
                .AddEntityFrameworkStores<IdentityContext>();
        }

        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new UserProfile());
                c.AddProfile(new AddressProfile());
                c.AddProfile(new CityProfile());
                c.AddProfile(new CountryProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddTrasitentServices(this IServiceCollection services)
        {
            services.AddTransient<DatabaseSeeder>();
        }

        public static void AddSwagger(this IServiceCollection services)
        {
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
    }
}
