namespace RecipeManagement.Controllers.v1;

using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeManagement.Domain.Users;
using RecipeManagement.Domain.Users.Dtos;
using RecipeManagement.Services;

[ApiController]
[Route("api/v{v:apiVersion}/auth")]
[ApiVersion("1.0")]
public sealed class AuthController(UserManager<ApplicationUser> userManager, ITokenService tokenService) : ControllerBase
{
    /// <summary>
    /// Creates a new user account.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("register", Name = "Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = new ApplicationUser { UserName = registerDto.Email, Email = registerDto.Email };
        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem(ModelState);
        }

        return StatusCode(StatusCodes.Status201Created, new { user.Id, user.Email });
    }

    /// <summary>
    /// Validates credentials and returns a JWT access token.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("login", Name = "Login")]
    public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);

        // same response for unknown email, wrong password and locked account
        var invalidLogin = Problem(statusCode: StatusCodes.Status401Unauthorized, title: "Invalid email or password.");
        if (user is null || await userManager.IsLockedOutAsync(user))
        {
            return invalidLogin;
        }

        if (!await userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            await userManager.AccessFailedAsync(user);
            return invalidLogin;
        }

        await userManager.ResetAccessFailedCountAsync(user);
        return Ok(tokenService.CreateToken(user));
    }

    /// <summary>
    /// Returns the identity of the authenticated user.
    /// </summary>
    [Authorize]
    [HttpGet("me", Name = "GetCurrentUser")]
    public IActionResult Me()
    {
        return Ok(new
        {
            Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
            Email = User.FindFirstValue(ClaimTypes.Email)
        });
    }
}
