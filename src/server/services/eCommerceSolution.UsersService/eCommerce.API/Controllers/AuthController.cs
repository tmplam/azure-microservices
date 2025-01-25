using eCommerce.Core.Dtos;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    IUsersService _usersService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        if (registerRequest is null)
        {
            return BadRequest("Invalid registration data");
        }

        var response = await _usersService.RegisterAsync(registerRequest);

        if (response is null || response.Success == false)
        {
            return BadRequest(response);
        }
        
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        if (loginRequest is null)
        {
            return BadRequest("Invalid login data");
        }

        var response = await _usersService.LoginAsync(loginRequest);

        if (response is null || response.Success == false)
        {
            return Unauthorized(response);
        }

        return Ok(response);
    }
}
