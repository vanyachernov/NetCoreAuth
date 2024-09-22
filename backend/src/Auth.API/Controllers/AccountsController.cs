using Auth.API.Extensions;
using Auth.Application.Users.AuthenticateUser;
using Auth.Application.Users.CreateUser;
using Auth.Domain.Shared;
using Auth.Domain.UserManagement;
using Auth.Domain.UserManagement.ValueObjects;
using Auth.Infrastructure.Features.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController(UserManager<User> userManager) : ApplicationController
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] CreateUserRequest request,
        [FromServices] CreateUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(request.Email!);

        if (user is not null)
        {
            var conflictError = Error.NotFound("user.exists", "User with this email already exists");
            return conflictError.ToResponse();
        }

        var result = await handler.Handle(request, cancellationToken);
        
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.Value);
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(
        [FromBody] AuthenticateUserRequest request,
        [FromServices] JwtHandler handler,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(request.Email!);

        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password!))
        {
            return Unauthorized(new AuthenticateUserResponse { ErrorMessage = "Invalid Authentication" });
        }

        user.SetLastAuthDate(new LastAuthAt(DateTime.UtcNow));

        var token = handler.CreateToken(user);

        return Ok(new AuthenticateUserResponse
        {
            IsAuthSuccessful = true,  
            Token = token
        });
    }
}