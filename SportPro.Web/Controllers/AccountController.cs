using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
                return RedirectToAction("Register");
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

    private void ValidateRegisterViewModel(RegisterViewModel registerViewModel)
    {
        if (!registerViewModel.Email.EndsWith("@sportpro.ba"))
        {
            ModelState.AddModelError("Email", "Email mora završavati sa @sportpro.ba");
        }
    }
}
