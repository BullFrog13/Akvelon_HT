using System;
using System.Threading.Tasks;
using UserService.DAL.EF;
using UserService.DAL.Entities;
using UserService.DAL.Interfaces;
using UserService.DAL.Repositories;

namespace UserService.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        private CommonRepository<User> _userRepository;

        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _databaseContext = new DatabaseContext(connectionString);
        }

        public IRepository<User> Users => _userRepository ?? (_userRepository = new CommonRepository<User>(_databaseContext));

        public void Save()
        {
            _databaseContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _databaseContext.Dispose();
            }

            _disposed = true;
        }
    }
}
