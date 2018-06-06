using System;
using System.Threading.Tasks;
using Data.Core.Domain.Entities;

namespace Data.Core.Interfaces
{
    public interface IAnnouncementRepository : IGenericRepository<Announcement>
    {
        Task<Announcement> GetByIdAsync(Guid id);
    }
}
