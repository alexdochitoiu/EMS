using System;
using System.Threading.Tasks;
using Data.Core.Domain.Entities;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories
{
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Announcement> GetByIdAsync(Guid id)
        {
            return _context.Announcements
                .Include(t => t.User)
                .FirstAsync(a => a.Id == id);
        }
    }
}
