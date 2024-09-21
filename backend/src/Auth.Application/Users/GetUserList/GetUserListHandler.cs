using Auth.Application.Users.CreateUser;
using Auth.Domain.Shared;
using Auth.Domain.UserManagement;
using CSharpFunctionalExtensions;

namespace Auth.Application.Users.GetUserList;

public class GetUserListHandler(IUsersRepository usersRepository)
{
    public async Task<Result<List<GetUserListResponse>, Error>> Handle(
        CancellationToken cancellationToken = default)
    {
        var getUsersResult = await usersRepository.GetUsers(cancellationToken);

        if (getUsersResult.IsFailure)
        {
            return Errors.General.NotFound();
        }

        return getUsersResult.Value;
    }
}