using Auth.Application.Users;
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
        
    public async Task<Result<Guid, string>> Register(User user, string hashedPassword, CancellationToken cancellationToken = default)
    {
        var result = await _userManager.CreateAsync(user, hashedPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure<Guid, string>(errors);
        }
        
        if (!Guid.TryParse(user.Id, out Guid userId))
        {
            return Result.Failure<Guid, string>("Invalid user ID format!");
        }

        return Result.Success<Guid, string>(userId);
    }

    public async Task<Result<User, string>> GetByEmail(Email email, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Email == email.Value, cancellationToken);

        if (user is null)
        {
            return Result.Failure<User, string>("User isn't exists");
        }

        return Result.Success<User, string>(user);
    }

    public Task SetStatusAsync(Guid userId, bool deletedStatus, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}