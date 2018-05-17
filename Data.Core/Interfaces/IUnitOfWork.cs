using System;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICountryRepository Countries { get; }
        ICityRepository Cities { get; }
        Task<int> Complete();
    }
}
