using GuessingGame.BL.Services;
using GuessingGame.Model.Entities;
using GuessingGame.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GuessingGame.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthService authService;
        private readonly IConfiguration configuration;

        public AccountController(IAuthService authService,
            UserManager<User> userManager, IConfiguration configuration) : base(userManager)
        {
            this.authService = authService;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var authResult = await authService.LoginOrRegister(model.UserName.Trim().ToLower());

                if (!authResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, authResult.Errors.FirstOrDefault());
                    return View(model);
                }

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(authResult.ClaimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleAuth"), Items = { new KeyValuePair<string, string>("LoginProvider", "google") } };
            return Challenge(properties, "Google");
        }

        public async Task<IActionResult> GoogleAuth()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result.Succeeded)
            {
                var externalUser = result.Principal;
                if (externalUser == null)
                {
                    return StatusCode(500, "External authentication error");
                }

                var extclaims = externalUser.Claims.ToList();

                var userIdClaim = extclaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return StatusCode(500, "Unknown userid");
                }


                var externalProvider = userIdClaim.Issuer;
                if (externalProvider.Trim().ToLower() != "google")
                    return StatusCode(500, "Incorrect provider");

                var emailClaims = extclaims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
                if (emailClaims == null)
                    return StatusCode(500, "Email address not configured in your social accounts");

                var nameClaims = extclaims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

                var authResult = await authService.GoogleLogin(externalProvider, userIdClaim.Value, emailClaims.Value, nameClaims.Value);

                if (!authResult.Succeeded)
                    return StatusCode(500, authResult.Errors);

                return RedirectToAction("Index", "Home");
            }

            return StatusCode(500, result.Failure.Message);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (HttpContext.User.Identity.AuthenticationType.Trim().ToLower() == "google")
                return Redirect(configuration.GetSection("Google")["LogoutUrl"]);

            return RedirectToAction("Login", "Account");
        }
    }
}
