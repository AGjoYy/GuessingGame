using System;
using System.Collections.Generic;
using System.Text;

namespace GuessingGame.DAL
{
    public class Result
    {
        public string Error { get; set; }

        public List<string> Errors { get; set; }

        public bool Succeeded { get; set; }

        public Result(bool succeed) => Succeeded = succeed;

        public Result(bool succeed, string error) : this(succeed) => Error = error;

        public Result(bool succeed, List<string> errors) : this(succeed) => Errors = errors;
    }
}
