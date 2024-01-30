// Copyright (C) <2024>  <ODINDash>
// 
// This file is part of SECODash.
// 
// SECODash is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SECODash is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Dtos.User;
using SECODashBackend.Models;
using SECODashBackend.Services.Users;
using Swashbuckle.AspNetCore.Annotations;


namespace SECODashBackend.Controllers;
/// <summary>
/// This controller is responsible for handling requests related to users.
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController(ILogger<UsersController> logger, UsersService usersService) : ControllerBase
{
    /// <summary>
    /// Return all Users saved in the database.
    /// </summary>
    [HttpGet("GetAll")]
    public async Task<List<User>> GetAllAsync()
    {
        var result = await usersService.GetAllAsync();
        return result;
    }
    /// <summary>
    /// Add a User to the database.
    /// </summary>
    [HttpPost("AddUser")]
    public async Task AddUserAsync(string id, string userName)
    {
        await usersService.AddUserAsync(id, userName);
    }
    /// <summary>
    /// Get a User using the provided UserName.
    /// </summary>
    [HttpPost("GetByName") ]
    public async Task<ActionResult<User>> GetByNameAsync(string userName)
    {
        var result = await usersService.GetByNameAsync(userName);
        return result == null ? NotFound() : Ok(result);
    }
    /// <summary>
    /// Remove a User by by provided Id.
    /// </summary>
    [HttpPost("RemoveUser")]
    public async Task RemoveUser(string id)
    {
        await usersService.RemoveById(id);
    }
    /// <summary>
    /// Handle a login request by checking the user database and returning the UserType.
    /// </summary>
    [HttpPost("LoginRequest")]
    [SwaggerOperation("Return user type and ecosystems that the user can edit.")]
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public async Task<ActionResult<UserPermissionsDto>> LoginRequest(UserTypeRequestDto req)
    {
        var result = await usersService.LoginRequest(req.Id, req.Username);

    return new ObjectResult(result);
    }
    /// <summary>
    /// Handle the request for changing the users permissions level.
    /// </summary>
    [HttpPost("UpdatePermissions")]
    [SwaggerOperation("Update the permission level of a User")]
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public async Task<string> UpdatePermissions(UpdatePermissionsDto dto)
    {
       var result = await usersService.UpdateUserType(dto.UserName, dto.UserType);
       return result;
    }
    /// <summary>
    /// Handle the request to give an 'Admin' editorial rights of an ecosystem.
    /// </summary>
    [HttpPost("PermitEditor")]
    [SwaggerOperation("Give Admin permission to edit a top-level ecosystem")]
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public async Task<string> PermitEditor(string rootAdminId, string userName, string topEcosystem)
    {
        return await usersService.AddEditorToEcosystem(rootAdminId,userName,topEcosystem);
    }
}