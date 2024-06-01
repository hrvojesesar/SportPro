using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Models.Domains;
using SportPro.Web.Models.ViewModels;

namespace SportPro.Web.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var userRoles = new List<Users>();
        foreach (IdentityUser user in users)
        {
            var thisViewModel = new Users();
            thisViewModel.UserId = user.Id;
            thisViewModel.UserName = user.UserName;
            thisViewModel.Email = user.Email;
            thisViewModel.Roles = await GetUserRoles(user);
            userRoles.Add(thisViewModel);

        }
        return View(userRoles);
    }

    private async Task<IEnumerable<string>> GetUserRoles(IdentityUser user)
    {
        return new List<string>(await _userManager.GetRolesAsync(user));
    }


    public async Task<IActionResult> Manage(string userId)
    {
        ViewBag.userId = userId;
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
            return NotFound();
        }
        ViewBag.UserName = user.UserName;

        // Materijalizacija liste u memoriju pre obrade
        var roles = _roleManager.Roles.ToList();
        var model = new List<Roles>();

        foreach (var role in roles)
        {
            var userRolesViewModel = new Roles
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Selected = await _userManager.IsInRoleAsync(user, role.Name)
            };
            model.Add(userRolesViewModel);
        }

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Manage(List<Roles> model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return View();
        }
        var roles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRolesAsync(user, roles);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Cannot remove user existing roles");
            return View(model);
        }
        result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Cannot add selected roles to user");
            return View(model);
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddUserRequest model)
    {
        ValidateRegisterViewModel(model);

        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);

    }

    [HttpGet]
    public async Task<IActionResult> Edit(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        var model = new EditUserRequest
        {
            UserName = user.UserName,
            Email = user.Email
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditUserRequest model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                user.UserName = model.UserName;
                user.Email = model.Email;

                ValidateRegisterModelForEdit(model);

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Users");
                }
                ModelState.AddModelError("", "User not updated, something went wrong.");
                return View(model);
            }
            return NotFound();
        }
        return View(model);

    }

    [HttpGet]
    public async Task<IActionResult> Delete(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        var model = new Users
        {
            UserName = user.UserName,
            Email = user.Email
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EditUserRequest model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Users");
            }
            ModelState.AddModelError("", "User not deleted, something went wrong.");
            return View(model);
        }
        return NotFound();
    }

    private void ValidateRegisterViewModel(AddUserRequest addUserRequest)
    {
        if (!addUserRequest.Email.EndsWith("@sportpro.ba"))
        {
            ModelState.AddModelError("Email", "Email mora biti sa domenom sportpro.ba");
        }
    }

    private void ValidateRegisterModelForEdit(EditUserRequest editUserRequest)
    {
        if (!editUserRequest.Email.EndsWith("@sportpro.ba"))
        {
            ModelState.AddModelError("Email", "Email mora biti sa domenom sportpro.ba");
        }
    }
}
