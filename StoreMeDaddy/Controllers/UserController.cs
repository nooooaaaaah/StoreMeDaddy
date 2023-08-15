namespace StoreMeDaddy.Controllers;
using Microsoft.AspNetCore.Mvc;
using StoreMeDaddy.Services;
using StoreMeDaddy.Models;
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
    public async Task<ActionResult<UserModel>> Get()
    {
        UserModel user = await _userService.GetUserAsync(User);
        return Ok(user);
    }
    [HttpPost]
    public async Task<ActionResult<UserModel>> Create(UserModel user, string password)
    {
        UserModel createdUser = await _userService.CreateUserAsync(user, password);
        return Ok(createdUser);
    }

    [HttpPut]
    public async Task<ActionResult<UserModel>> Update(UserModel user)
    {
        UserModel updatedUser = await _userService.UpdateUserAsync(user);
        return Ok(updatedUser);
    }

    [HttpDelete]
    public async Task<ActionResult<UserModel>> Delete()
    {
        UserModel user = await _userService.DeleteUserAsync(User);
        return Ok(user);
    }
}