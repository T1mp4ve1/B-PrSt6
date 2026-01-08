using Evento.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Amministratore")]
public class AdminController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("promote/{userId}")]
    public async Task<IActionResult> PromoteToAdmin(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound("Utente non trovato");

        const string adminRole = "Amministratore";

        if (!await _roleManager.RoleExistsAsync(adminRole))
        {
            var createRole = await _roleManager.CreateAsync(new AppRole { Name = adminRole, DescriptionRole = "Amministratore role" });
            if (!createRole.Succeeded)
                return BadRequest(createRole.Errors);
        }

        var currentRoles = await _userManager.GetRolesAsync(user);

        var rolesToRemove = currentRoles.Where(r => !string.Equals(r, adminRole, StringComparison.OrdinalIgnoreCase)).ToList();
        if (rolesToRemove.Any())
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!removeResult.Succeeded) return BadRequest(removeResult.Errors);
        }

        if (!await _userManager.IsInRoleAsync(user, adminRole))
        {
            var addResult = await _userManager.AddToRoleAsync(user, adminRole);
            if (!addResult.Succeeded) return BadRequest(addResult.Errors);
        }

        return Ok("Utente promosso");
    }
}