using Data.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Data.Core.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Country> Countries { get; set; }
        DatabaseFacade Database { get; }

        int SaveChanges();
    }
}
