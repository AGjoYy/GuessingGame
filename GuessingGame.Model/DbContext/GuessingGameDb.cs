using GuessingGame.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuessingGame.Model.DbContext
{
    public class GuessingGameDb : IdentityDbContext<User, Role, int>
    {
        public GuessingGameDb(DbContextOptions<GuessingGameDb> options) : base(options)
        {
        }
    }
}
