using System;
using API.Repositories.CustomRepositories;

namespace API.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete();
    }
}