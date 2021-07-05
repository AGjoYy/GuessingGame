using GuessingGame.DAL.IRepositories;
using GuessingGame.DAL.Repositories;
using GuessingGame.Model.DbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuessingGame.DAL
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IUserRepository userRepository;
        private readonly GuessingGameDb dbContext;

        public UnitOfWork(GuessingGameDb dbContext)
        {
            this.dbContext = dbContext;
        }

        public IUserRepository UserRepository => userRepository ?? new UserRepository(dbContext);

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                disposed = true;
            }
        }
    }
}
