using Auth.Application.Users;
using Auth.Domain.UserManagement;
using Auth.Domain.UserManagement.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly UserManager<User> _userManager;

    public UsersRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
        
    public async Task<Guid> Register(User user, string hashPassword, CancellationToken cancellationToken = default)
    {
        var result = await _userManager.CreateAsync(user, hashPassword);
    }

    public Task<Result<User, string>> GetByEmail(Email email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SetStatusAsync(Guid userId, bool deletedStatus, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}