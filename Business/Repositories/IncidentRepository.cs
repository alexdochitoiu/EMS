using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core.Domain.Entities;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories
{
    public class IncidentRepository : GenericRepository<Incident>, IIncidentRepository
    {
        private readonly ApplicationDbContext _context;

        public IncidentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Incident> GetByIdAsync(Guid id)
        {
            return _context.Incidents
                .Include(t => t.Reporter)
                .FirstAsync(a => a.Id == id);
        }

        public Task<List<Incident>> GetIncidentsWithinARadiusAsync(double centerLat, double centerLng, double km)
        {
            return _context.Incidents
                .Include(t => t.Reporter)
                .Where(i => i.IsNear(centerLat, centerLng, km))
                .ToListAsync();
        }
    }
}
