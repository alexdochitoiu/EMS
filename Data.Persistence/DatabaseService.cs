using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence
{
    public sealed class DatabaseService : DbContext
    {
        public DatabaseService(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
