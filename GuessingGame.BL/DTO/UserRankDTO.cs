using System;
using System.Collections.Generic;
using System.Text;

namespace GuessingGame.BL.DTO
{
    public class UserRankDTO
    {
        public string UserName { get; set; }

        public double SuccessRate { get; set; }

        public int TotalTries { get; set; }
    }
}
