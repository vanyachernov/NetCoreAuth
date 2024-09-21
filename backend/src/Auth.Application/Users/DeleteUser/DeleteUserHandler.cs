using Auth.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Auth.Application.Users.DeleteUser;

public class DeleteUserHandler(IUsersRepository usersRepository)
{
    public async Task<Result<Guid, Error>> Handle(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var resultDeletingUser = await usersRepository.DeleteAsync(userId, cancellationToken);

        return resultDeletingUser;
    }
}