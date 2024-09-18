using Auth.Application.Users.GetUserList;
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
}