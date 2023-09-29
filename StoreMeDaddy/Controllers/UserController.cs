namespace StoreMeDaddy.Controllers;
using Microsoft.AspNetCore.Mvc;
using StoreMeDaddy.Services;
// using StoreMeDaddy.Models;
using StoreMeDaddy.Objects;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<UserRecord>> Get()
    {
        UserRecord user = await _userService.GetUserAsync(User);
        return Ok(user);
    }
    [HttpPost]
    public async Task<ActionResult<UserRecord>> Create(UserRecord user, string password)
    {
        UserRecord createdUser = await _userService.CreateUserAsync(user, password);
        return Ok(createdUser);
    }

    [HttpPut]
    public async Task<ActionResult<UserRecord>> Update(ExistingUserRecord user)
    {
        UserRecord updatedUser = await _userService.UpdateUserAsync(user);
        return Ok(updatedUser);
    }

    [HttpDelete]
    public async Task<ActionResult<UserRecord>> Delete()
    {
        UserRecord user = await _userService.DeleteUserAsync(User);
        return Ok(user);
    }
}