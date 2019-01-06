using System;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IAnnouncementRepository Announcements { get; }
        IIncidentRepository Incidents { get; }
        ICountryRepository Countries { get; }
        ICityRepository Cities { get; }
        Task<int> CompleteAsync();
    }
}
