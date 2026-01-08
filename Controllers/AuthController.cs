using Evento.Models;
using Evento.Models.DTO.AuthDTO;
using Evento.Services.TokenSrvice;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly RoleManager<AppRole> _roleManager;

    public AuthController(UserManager<AppUser> userManager,
                          SignInManager<AppUser> signInManager,
                          ITokenService tokenService,
                          RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new AppUser
        {
            UserName = dto.UserName,
            Email = dto.Email,
            Nome = dto.Nome,
            Cognome = dto.Cognome,
            CreatedAt = DateTime.UtcNow,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        var roleExists = await _roleManager.RoleExistsAsync("Utente");
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new AppRole { Name = "Utente", DescriptionRole = "Utente role" });
        }

        var addRoleResult = await _userManager.AddToRoleAsync(user, "Utente");
        if (!addRoleResult.Succeeded)
        {
            return BadRequest(addRoleResult.Errors);
        }

        return Ok(new { user.Id, user.UserName, user.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user == null) return Unauthorized("Credenziali non valide");

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded) return Unauthorized("Credenziali non valide");

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.CreateToken(user, roles);

        return Ok(new { token });
    }
}