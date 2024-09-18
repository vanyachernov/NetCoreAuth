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
    public async Task<List<User>> GetAccounts(CancellationToken cancellationToken = default)
    {
        var users = new List<User>();
        return users;
    };
}