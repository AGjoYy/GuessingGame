using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame.Models
{
    public class UserAnswerModel
    {
        public string Number { get; set; }
        public int TriesLeft { get; set; }
    }
}
