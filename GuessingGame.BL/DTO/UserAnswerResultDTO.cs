using System;
using System.Collections.Generic;
using System.Text;

namespace GuessingGame.BL.DTO
{
    public class UserAnswerResultDTO
    {
        //m - number of matching digits but not on the right places
        public int CorrectDigitsNotOnTheRightPlace { get; set; }
        //p - number of matching digits on exact places
        public int CorrectDigitsOnTheRightPlace { get; set; }

        public bool IsWinner { get; set; }
    }
}
