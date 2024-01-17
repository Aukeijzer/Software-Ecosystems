using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Dtos.User;
using SECODashBackend.Models;
using SECODashBackend.Services.Users;
using Swashbuckle.AspNetCore.Annotations;


namespace SECODashBackend.Controllers;
/// <summary>
/// This controller is responsible for handling requests related to users.
/// </summary>
/// <param name="logger"></param>
/// <param name="usersService"></param>
[ApiController]
[Route("[controller]")]
public class UsersController(ILogger<UsersController> logger, UsersService usersService) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<List<User>> GetAllAsync()
    {
        var result = await usersService.GetAllAsync();
        return result;
    }

    [HttpPost("AddUser")]
    public async Task AddUserAsync(string id, string userName)
    {
        await usersService.AddUserAsync(id, userName);
    }

    [HttpPost("GetByName") ]
    public async Task<ActionResult<User>> GetByNameAsync(string userName)
    {
        var result = await usersService.GetByNameAsync(userName);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("RemoveUser")]
    public async Task RemoveUser(string id)
    {
        await usersService.RemoveById(id);
    }

    [HttpPost("LoginRequest")]
    [SwaggerOperation("Return user type")]
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public async Task<ActionResult<UserTypeDto>> LoginRequest(UserTypeRequestDto req)
    {
        var result = await usersService.LoginRequest(req.Id, req.Username);
        var final = new UserTypeDto
        {
            UserType = result
        };
    return new ObjectResult(final);
    }

 
    
}