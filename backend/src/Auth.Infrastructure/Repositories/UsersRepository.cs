using Auth.Application.DTOs;
using Auth.Application.Users;
using Auth.Application.Users.GetUserList;
using Auth.Domain.Shared;
using Auth.Domain.UserManagement;
using Auth.Domain.UserManagement.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure.Repositories;

public class UsersRepository(UserManager<User> userManager) : IUsersRepository
{
    public async Task<Result<Guid, Error>> Register(
        User user, 
        string password, 
        CancellationToken cancellationToken = default)
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

    public async Task<Result<Guid, Error>> SetStatusAsync(
        Guid userId, 
        IsDeleted deletedStatus, 
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return Errors.General.NotFound(userId);
        }

        user.SetDeletedStatus(deletedStatus);

        await userManager.UpdateAsync(user);

        return Result.Success<Guid, Error>(userId);
    }

    public Task<Result<List<GetUserListResponse>>> GetUsers(CancellationToken cancellationToken = default)
    {
        var users = userManager.Users.ToList();

        var userListResponse = users.Select(user => new GetUserListResponse
        {
            Id = Guid.Parse(user.Id),
            FullName = new FullNameDto(user.FullName.FirstName, user.FullName.LastName),
            RegisterAt = new RegisterAtDto(user.RegisterAt.Date),
            LastAuthAt = new LastAuthAtDto(user.LastAuthAt.Date),
            Status = new IsDeletedDto(user.IsDeleted.Status)
        }).ToList();

        return Task.FromResult(Result.Success(userListResponse));
    }
}