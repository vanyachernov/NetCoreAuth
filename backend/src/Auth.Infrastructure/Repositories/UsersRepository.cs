using Auth.Application.Users;
using Auth.Domain.Shared;
using Auth.Domain.UserManagement;
using Auth.Domain.UserManagement.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories;

public class UsersRepository(UserManager<User> userManager) : IUsersRepository
{
    public async Task<Result<Guid, Error>> Register(User user, string password, CancellationToken cancellationToken = default)
    {
        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return Errors.General.ValueIsInvalid("User register");
        }
        
        if (!Guid.TryParse(user.Id, out Guid userId))
        {
            return Errors.General.ValueIsInvalid("UserId");
        }

        return Result.Success<Guid, Error>(userId);
    }

    public Task SetStatusAsync(Guid userId, bool deletedStatus, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}