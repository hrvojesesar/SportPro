using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportPro.Web.Data;
using SportPro.Web.Models.Domains;

namespace SportPro.Web.Controllers;

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
}
