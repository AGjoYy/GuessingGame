using GuessingGame.DAL;
using GuessingGame.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GuessingGame.BL.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAll();

        Task<User> GetById(int id);

        Task<List<User>> GetByCondition(Expression<Func<User, bool>> expression);

        Task<Result> Update(User user);

        Task<Result> Remove(User user);
    }
    public class UserService : IUserService
    {
        private readonly IUnitOfWork repository;

        public UserService(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<List<User>> GetAll()
        {
            return await repository.UserRepository.GetAll().ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await repository.UserRepository.GetById(id);
        }

        public async Task<List<User>> GetByCondition(Expression<Func<User, bool>> expression)
        {
            return await repository.UserRepository.GetByCondition(expression).ToListAsync();
        }

        public async Task<Result> Update(User user)
        {
            return await repository.UserRepository.Update(user);
        }

        public async Task<Result> Remove(User user)
        {
            return await repository.UserRepository.Remove(user);
        }
    }
}
