using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;


public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }


    [HttpGet]
    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        ValidateRegisterViewModel(registerViewModel);
        var identityUser = new IdentityUser
        {
            UserName = registerViewModel.Username,
            Email = registerViewModel.Email
        };

        var result = await _userManager.CreateAsync(identityUser, registerViewModel.Password);

        if (result.Succeeded)
        {
            // assign this user the "Uposlenik" role
            var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "Uposlenik");

            if (roleIdentityResult.Succeeded)
            {
                // Show success notification
                return RedirectToAction("Index", "Home");
            }
        }

        // Show error notification
        return View();


    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

        if (signInResult != null && signInResult.Succeeded)
        {
            // Show success notification
            return RedirectToAction("Index", "Home");
        }

        // Show error notification
        return View();

    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> AccessDenied()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordRequest.CurrentPassword, changePasswordRequest.NewPassword);

            if (result.Succeeded)
            {
                _logger.LogInformation("User changed their password successfully.");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        // Show error notification
        return View();
    }

    private void ValidateRegisterViewModel(RegisterViewModel registerViewModel)
    {
        if (!registerViewModel.Email.EndsWith("@sportpro.ba"))
        {
            ModelState.AddModelError("Email", "Email mora završavati sa @sportpro.ba");
        }
    }
}
