using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain.Entities;

namespace Data.Core.Interfaces
{
    public interface IIncidentRepository : IGenericRepository<Incident>
    {
        Task<Incident> GetByIdAsync(Guid id);
        Task<List<Incident>> GetIncidentsWithinARadiusAsync(double centerLat, double centerLng, double km);
    }
}
