using Auth.Application.Users.CreateUser;
using Auth.Domain.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ApplicationController
{
    private readonly UserManager<User> _userManager;
    
    public AccountsController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] CreateUserRequest request,
        [FromServices] CreateUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            return BadRequest();
        }

        var result = await handler.Handle(request, cancellationToken);
        
        if (result.IsFailure)
        {
            var errors = result.Error.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(error => error.Trim())
                .ToList();

            return BadRequest(new CreateUserResponse { Errors = errors });
        }

        return Ok(result.Value);
    }
    
}