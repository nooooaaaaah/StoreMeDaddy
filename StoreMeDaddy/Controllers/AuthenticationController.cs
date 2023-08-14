namespace StoreMeDaddy.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoinMachine;
using StoreMeDaddy.Services;
using Microsoft.Extensions.DependencyInjection;
using StoreMeDaddy.Models;
using StoreMeDaddy.Adapters;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    [ActivatorUtilitiesConstructor]
    public AuthenticationController(ITokenService tokenService, IUserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticationModel authentication)
    {
        UserModel user = await _userService.Authenticate(authentication.Username, authentication.Password);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });
        IUser userAdapter = new UserSessionAdapter(user);
        string token = _tokenService.GenerateToken(userAdapter);
        return Ok(new { Token = token });
    }

}
