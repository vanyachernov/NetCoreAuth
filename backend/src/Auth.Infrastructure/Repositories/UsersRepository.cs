using Auth.Application.Users;
using Auth.Domain.Shared;
using Auth.Domain.UserManagement;
using Auth.Domain.UserManagement.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly UserManager<User> _userManager;

    public UsersRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
        
    public async Task<Result<Guid, Error>> Register(User user, string hashedPassword, CancellationToken cancellationToken = default)
    {
        var result = await _userManager.CreateAsync(user, hashedPassword);

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

    public async Task<Result<User, Error>> GetByEmail(Email email, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Email == email.Value, cancellationToken);

        if (user is null)
        {
            return Errors.General.NotFound();
        }

        return Result.Success<User, Error>(user);
    }

    public Task SetStatusAsync(Guid userId, bool deletedStatus, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}