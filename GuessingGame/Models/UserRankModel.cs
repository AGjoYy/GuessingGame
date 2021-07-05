using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.Models
{
    public class UserRankModel
    {
        public string UserName { get; set; }

        public double SuccessRate { get; set; }

        public int TotalTries { get; set; }
    }
}
