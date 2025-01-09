using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOwnerRepository OwnerRepository { get; }
        IPropertyRepository PropertyRepository { get; }
        IPropertyTraceRepository PropertyTraceRepository { get; }
        IPropertyImageRepository PropertyImageRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> CommitAsync();
    }
}
