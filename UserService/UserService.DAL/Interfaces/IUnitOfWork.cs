using System;
using System.Threading.Tasks;
using UserService.DAL.Entities;

namespace UserService.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }

        void Save();

        Task SaveAsync();
    }
}