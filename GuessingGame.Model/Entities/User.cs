using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuessingGame.Model.Entities
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }

        public int RandomNumber { get; set; }

        public int AttemptsCount { get; set; }

        public int WonGamesCount { get; set; }

        public int PlayedGamesCount { get; set; }
    }
}
