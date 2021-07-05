using GuessingGame.Model.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GuessingGame.BL.Services
{
    public interface IAuthService
    {
        Task<AuthenticationClaimsResult> LoginOrRegister(string userName);
        Task<AuthenticationResult> GoogleLogin(string provider, string providerKey, string email, string fullName);
    }
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;

        public AuthService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<AuthenticationClaimsResult> LoginOrRegister(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                var result = await CreateNewUser(userName);

                if (!result.Succeeded)
                {
                    return new AuthenticationClaimsResult { Succeeded = false, Errors = result.Errors.Select(er => er.Description).ToList() };
                }
                else
                {
                    user = await userManager.FindByNameAsync(userName);
                }
            }

            return Authenticate(user);
        }

        public async Task<AuthenticationResult> GoogleLogin(string provider, string providerKey, string email, string fullName)
        {
            User user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                var result = await userManager.CreateAsync(new User
                {
                    UserName = email,
                    Email = email,
                    FullName = fullName
                });

                if (!result.Succeeded)
                {
                    return new AuthenticationResult { Succeeded = false, Errors = result.Errors.Select(er => er.Description).ToList() };
                }
                user = await userManager.FindByEmailAsync(email);
            }

            if (await userManager.FindByLoginAsync(provider, providerKey) == null)
            {
                await userManager.AddLoginAsync(user, new UserLoginInfo(provider, providerKey, email));
            }

            return new AuthenticationResult { Succeeded = true };
        }

        private async Task<IdentityResult> CreateNewUser(string userName)
        {
            return await userManager.CreateAsync(new User { UserName = userName });
        }

        private AuthenticationClaimsResult Authenticate(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return new AuthenticationClaimsResult { Succeeded = true, ClaimsIdentity = claimsIdentity };
        }
    }
}
