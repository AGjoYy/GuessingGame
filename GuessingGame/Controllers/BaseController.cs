using GuessingGame.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GuessingGame.Controllers
{
    [Authorize]
    [Controller]
    public class BaseController : Controller
    {
        private readonly UserManager<User> userManager;

        public BaseController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        private User CurrentUser { get; set; }
        protected async Task<User> GetCurrentUser()
        {
            try
            {
                if (string.IsNullOrEmpty(User.Identity.Name))
                {
                    CurrentUser = null;
                }
                else if (CurrentUser == null || CurrentUser.UserName.Trim().ToLower() != User.Identity.Name.Trim().ToLower())
                {
                    CurrentUser = await userManager.FindByNameAsync(User.Identity.Name);
                }

                if (CurrentUser == null && User.Identity.AuthenticationType.Trim().ToLower() != "applicationcookie")
                {
                    var email = User.Identities.FirstOrDefault().Claims.ToList().FirstOrDefault(x => x.Type == ClaimTypes.Email);
                    CurrentUser = await userManager.FindByEmailAsync(email.Value);
                }
                return CurrentUser;
            }
            catch
            {
                return null;
            }
        }
    }
}
