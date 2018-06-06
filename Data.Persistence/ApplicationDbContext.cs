using System;
using Data.Core.Domain.Entities;
using Data.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence
{
    public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions options) 
            : base(options) => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<UserClaim>().ToTable("UserClaims");
            builder.Entity<UserLogin>().ToTable("UserLogins");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserToken>().ToTable("UserTokens");
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Incident> Incidents { get; set; }
    }
}
