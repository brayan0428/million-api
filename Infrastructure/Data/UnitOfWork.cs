using Core.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MillionDbContext _context;
        public UnitOfWork(MillionDbContext context)
        {
            _context = context;
        }

        public IOwnerRepository OwnerRepository => new OwnerRepository(_context);
        public IPropertyRepository PropertyRepository => new PropertyRepository(_context);
        public IPropertyImageRepository PropertyImageRepository => new PropertyImageRepository(_context);
        public IPropertyTraceRepository PropertyTraceRepository => new PropertyTraceRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
