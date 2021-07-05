using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace GuessingGame.BL
{

    public class AuthenticationResult
    {
        public bool Succeeded { get; set; }

        public List<string> Errors { get; set; }
    }

    public class AuthenticationClaimsResult : AuthenticationResult
    {
        public ClaimsIdentity ClaimsIdentity { get; set; }


    }
}
