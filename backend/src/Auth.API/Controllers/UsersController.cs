using Auth.Application.Users.GetUserList;
using Auth.Application.Users.SetDeletedStatus;
using Auth.Domain.UserManagement;
using Microsoft.AspNetCore.Authorization;
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
        CancellationToken cancellationToken = default)
    {
        var users = await handler.Handle(cancellationToken);
        
        return Ok(users.Value);
    }
    
    [HttpPost("{userId:guid}/isDeleted")]
    [Authorize]
    public async Task<ActionResult<Guid>> ChangeUserStatus(
        [FromRoute] Guid userId,
        [FromBody] ChangeUserStatusRequest request,
        [FromServices] ChangeUserStatusHandler handler,
        CancellationToken cancellationToken = default)
    {
        var userChangeStatusResult = await handler.Handle(
            userId, 
            request, 
            cancellationToken);

        if (userChangeStatusResult.IsFailure)
        {
            return BadRequest("Failed to change status");
        }
        
        return Ok(userChangeStatusResult.Value);
    }
}