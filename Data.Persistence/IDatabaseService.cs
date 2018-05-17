using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Data.Persistence
{
    public interface IDatabaseService
    {
        DbSet<User> Users { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Country> Countries { get; set; }
        DatabaseFacade Database { get; }

        int SaveChanges();
    }
}
