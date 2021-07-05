using GuessingGame.DAL.IRepositories;
using GuessingGame.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame.DAL.IRepositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
    }
}
