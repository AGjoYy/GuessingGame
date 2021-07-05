using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.Models
{
    public class LeaderboardModel
    {
        public string CurrentUserName { get; set; }

        public List<UserRankModel> Users { get; set; }
    }
}
