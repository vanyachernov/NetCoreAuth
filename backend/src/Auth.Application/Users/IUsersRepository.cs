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
    /// <param name="hashPassword">Hash password.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Guid> Register(User user, string hashPassword, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if user exists by his email.
    /// </summary>
    /// <param name="email">Email.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task<Result<User, string>> GetByEmail(Email email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets deleted status for a user.
    /// </summary>
    /// <param name="userId">A user identifier.</param>
    /// <param name="deletedStatus">Status.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>A <see cref="Task{User}"/>.</returns>
    Task SetStatusAsync(Guid userId, bool deletedStatus, CancellationToken cancellationToken = default);
}