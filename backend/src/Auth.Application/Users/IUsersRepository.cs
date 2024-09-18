using Auth.Application.Users.GetUserList;
using Auth.Domain.Shared;
using Auth.Domain.UserManagement;
using Auth.Domain.UserManagement.ValueObjects;
using CSharpFunctionalExtensions;

namespace Auth.Application.Users;

public interface IUsersRepository
{
    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="user">A user.</param>
    /// <param name="password">Hash password.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Result<Guid, Error>> Register(User user, string password, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets deleted status for a user.
    /// </summary>
    /// <param name="userId">A user identifier.</param>
    /// <param name="deletedStatus">Status.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Result<Guid, Error>> SetStatusAsync(Guid userId, IsDeleted deletedStatus, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets user list.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Result<List<GetUserListResponse>>> GetUsers(CancellationToken cancellationToken = default);
}