using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.Models
{
    public class UserAnswerResultModel
    {
        //m - number of matching digits but not on the right places
        public int CorrectDigitsNotOnTheRightPlace { get; set; }
        //p - number of matching digits on exact places
        public int CorrectDigitsOnTheRightPlace { get; set; }

        public bool IsWinner { get; set; }

        public int? CorrectNumber { get; set; }
    }
}
