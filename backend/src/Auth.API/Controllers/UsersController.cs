using Auth.Application.DTOs;
using Auth.Application.Users.DeleteUser;
using Auth.Application.Users.GetUserList;
using Auth.Application.Users.SetDeletedStatus;
using Auth.Domain.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ApplicationController
{
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<GetUserListResponse>>> GetAccounts(
        [FromServices] GetUserListHandler handler,
        [FromServices] UserManager<User> userManager,
        CancellationToken cancellationToken = default)
    {
        if (!Request.Headers.TryGetValue("UserId", out var userIdHeader) || 
            !Guid.TryParse(userIdHeader, out var currentUserId))
        {
            return BadRequest("Invalid or missing UserId in header");
        }

        var user = await userManager.FindByIdAsync(currentUserId.ToString());

        if (user is null)
        {
            return BadRequest("User doesn't exists!");
        }

        if (user.IsDeleted.Status)
        {
            return Forbid();
        }
        
        var users = await handler.Handle(cancellationToken);
        
        return Ok(users.Value);
    }
    
    [HttpPost("{userId:guid}/isDeleted")]
    [Authorize]
    public async Task<ActionResult<Guid>> ChangeUserStatus(
        [FromRoute] Guid userId,
        [FromBody] ChangeUserStatusRequest createUserRequest,
        [FromServices] ChangeUserStatusHandler handler,
        [FromServices] UserManager<User> userManager,
        CancellationToken cancellationToken = default)
    {
        if (!Request.Headers.TryGetValue("UserId", out var userIdHeader) || 
            !Guid.TryParse(userIdHeader, out var currentUserId))
        {
            return BadRequest("Invalid or missing UserId in header");
        }

        var user = await userManager.FindByIdAsync(currentUserId.ToString());

        if (user is null)
        {
            return BadRequest("User doesn't exists!");
        }

        if (user.IsDeleted.Status)
        {
            return Forbid();
        }
        
        var userChangeStatusResult = await handler.Handle(
            userId, 
            createUserRequest, 
            cancellationToken);

        if (userChangeStatusResult.IsFailure)
        {
            return BadRequest("Failed to change status");
        }
        
        return Ok(userChangeStatusResult.Value);
    }

    [HttpDelete("{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(
        [FromRoute] Guid userId,
        [FromServices] DeleteUserHandler handler,
        [FromServices] UserManager<User> userManager,
        CancellationToken cancellationToken = default)
    {
        if (!Request.Headers.TryGetValue("UserId", out var userIdHeader) || 
            !Guid.TryParse(userIdHeader, out var currentUserId))
        {
            return BadRequest("Invalid or missing UserId in header");
        }

        var user = await userManager.FindByIdAsync(currentUserId.ToString());

        if (user is null)
        {
            return BadRequest("User doesn't exists!");
        }

        if (user.IsDeleted.Status)
        {
            return Forbid();
        }

        var userDeleteResult = await handler.Handle(userId, cancellationToken);

        if (userDeleteResult.IsFailure)
        {
            return BadRequest("Failed deleting user!");
        }

        return Ok(userDeleteResult.Value);
    }
}