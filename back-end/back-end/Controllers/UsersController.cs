using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Models;
using SECODashBackend.Services.Users;

namespace SECODashBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(ILogger<UsersController> logger, UsersService usersService) : ControllerBase
{
    [HttpGet]
    public async Task<List<User>> GetAllAsync()
    {
        var result = await usersService.GetAllAsync();
        return result;
    }

    [HttpPost("Add User")]
    public async Task AddUserAsync(string[] user)
    {
        await usersService.AddUserAsync(user);
    }

    [HttpPost("Get By Name") ]
    public async Task<ActionResult<User>> GetByNameAsync(string userName)
    {
        var result = await usersService.GetByNameAsync(userName);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("Remove User")]
    public async Task RemoveUser(string id)
    {
        await usersService.RemoveById(id);
    }
}