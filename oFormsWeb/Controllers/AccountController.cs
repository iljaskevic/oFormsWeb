using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using oFormsWeb.Repositories;

namespace oFormsWeb.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : BaseController
    {
        private readonly AzureAdB2COptions _options;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IOptions<AzureAdB2COptions> b2cOptions, IUserRepository userRepository, ILogger<AccountController> logger)
        {
            _options = b2cOptions.Value;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            var redirectUrl = Url.Action(nameof(AccountController.PostSignIn), "Account");
            return Challenge(
                new AuthenticationProperties { RedirectUri = redirectUrl },
                OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public IActionResult PostSignIn()
        {
            _logger.LogDebug("**** Post Sign-Up ***");
            if (User.Identity.IsAuthenticated)
            {
                _logger.LogDebug("User claim****(New User): " + User.FindFirstValue("newUser"));
                if (!String.IsNullOrWhiteSpace(User.FindFirstValue("newUser")))
                {
                    _logger.LogDebug("New user - Adding to user table - " + GetUserObjectId());
                    _userRepository.InsertNewUser(GetUserObjectId());
                    return RedirectToAction(nameof(FormController.Index), "Form");
                }
            }
            return RedirectToAction(nameof(FormController.Index), "Form");
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            var redirectUrl = Url.Action(nameof(HomeController.Index), "Home");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            properties.Items[AzureAdB2COptions.PolicyAuthenticationProperty] = _options.ResetPasswordPolicyId;
            return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            var redirectUrl = Url.Action(nameof(HomeController.Index), "Home");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            properties.Items[AzureAdB2COptions.PolicyAuthenticationProperty] = _options.EditProfilePolicyId;
            return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            var callbackUrl = Url.Action(nameof(SignedOut), "Account", values: null, protocol: Request.Scheme);
            return SignOut(new AuthenticationProperties { RedirectUri = callbackUrl },
                CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public IActionResult SignedOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to home page if the user is authenticated.
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
