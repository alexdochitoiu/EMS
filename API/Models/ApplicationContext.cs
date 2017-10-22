using API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) 
            : base(options) {}

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
