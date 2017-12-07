using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence
{
    public interface IDatabaseService
    {
        DbSet<User> Users { get; set; }

        int SaveChangesAsync();
    }
}
