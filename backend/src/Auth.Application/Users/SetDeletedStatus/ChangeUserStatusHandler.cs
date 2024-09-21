using Auth.Domain.Shared;
using Auth.Domain.UserManagement.ValueObjects;
using CSharpFunctionalExtensions;

namespace Auth.Application.Users.SetDeletedStatus;

public class ChangeUserStatusHandler(IUsersRepository usersRepository)
{
    public async Task<Result<Guid, Error>> Handle(
        Guid userId,
        ChangeUserStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var userStatus = new IsDeleted(request.Status.Value);
        
        var userChangeStatusResult = await usersRepository.SetStatusAsync(
            userId, 
            userStatus, 
            cancellationToken);

        if (userChangeStatusResult.IsFailure)
        {
            return Errors.General.ValueIsInvalid("ChangeStatus");
        }

        return userChangeStatusResult.Value;
    }
}