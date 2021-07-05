using GuessingGame.DAL.IRepositories;
using GuessingGame.Model.DbContext;
using GuessingGame.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        public UserRepository(GuessingGameDb dbContext) : base(dbContext)
        {
        }
    }
}
