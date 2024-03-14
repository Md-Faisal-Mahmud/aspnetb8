using FirstDemo.Persistence.Features.Membership;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using Autofac;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Authentication;
using FirstDemo.Application.Features.Training.Services;
using FirstDemo.Infrastructure.Securities;

namespace FirstDemo.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailMessageService _emailMessageService;
        private readonly ITokenService _tokenService;

        public AccountController(ILifetimeScope scope, 
            ILogger<AccountController> logger,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IEmailMessageService emailMessageService,
            ITokenService tokenService
            )
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailMessageService = emailMessageService;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> RegisterAsync(string returnUrl = null)
        {
            var model = _scope.Resolve<RegisterModel>();
            model.ReturnUrl = returnUrl;
            //model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");
            //model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                { 
                    UserName = model.Email, 
                    Email = model.Email, 
                    FirstName = model.FirstName, 
                    LastName = model.LastName 
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        values: new { area = "", userId = user.Id, code = code, returnUrl = model.ReturnUrl },
                        protocol: Request.Scheme);

                    await _emailMessageService.SendEmailConfirmationEmailAsync(model.Email, 
                        $"{model.FirstName} {model.LastName}", callbackUrl);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToAction("RegisterConfirmation", new { email = model.Email, returnUrl = model.ReturnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(model.ReturnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            var model = _scope.Resolve<LoginModel>();

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var claims = (await _userManager.GetClaimsAsync(user)).ToArray();
                    var token = await _tokenService.GetJwtToken(claims);
                    HttpContext.Session.SetString("token", token);

                    return LocalRedirect(model.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
