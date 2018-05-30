using System.Text;
using Business;
using Data.Core.Domain.Entities.Identity;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using WebAPI.Infrastructure.Email.SendGrid;
using WebAPI.Mappings;
using WebAPI.Seeders;

namespace WebAPI.Infrastructure
{
    public static class ConfigureContainerExtensions
    {
        public static void AddDbContext(this IServiceCollection services)
        {
            var dbConnection = IocContainer.Configuration.GetConnectionString("EMSConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(dbConnection));

            services.AddIdentity<ApplicationUser, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
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
            services.AddTransient<IEmailSender, SendGridEmailSender>();
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

        public static void SetRegisterPolicy(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            });
        }

        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "x",// IocContainer.Configuration["JWTAuth:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = "x",// IocContainer.Configuration["JWTAuth:Audience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12345678987654321"))
                    };
                });
        }
    }
}
