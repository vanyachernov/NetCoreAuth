using Auth.Domain.UserManagement;
using Auth.Domain.UserManagement.ValueObjects;
using CSharpFunctionalExtensions;

namespace Auth.Application.Users.CreateUser;

public class CreateUserHandler(IUsersRepository userRepository)
{
    public async Task<Result<Guid, string>> Handle(
        CreateUserRequest request,
        CancellationToken cancellationToken = default)
    {

        var fullName = FullName.Create(
            request.FullNameDto.FirstName, 
            request.FullNameDto.LastName).Value;

        var isDeletedStatus = new IsDeleted(false);

        var lastAuthAt = new LastAuthAt(DateTime.UtcNow);

        var email = Email.Create(request.Email).Value;
        
        var existingUser = await userRepository.GetByEmail(
            email, 
            cancellationToken);

        if (existingUser.IsSuccess)
        {
            return Result.Failure<Guid, string>("User already exists");
        }
        
        var userToCreate = User.Create(
            fullName,
            isDeletedStatus,
            lastAuthAt,
            email);
        
        if (userToCreate.IsFailure)
        {
            return userToCreate.Error;
        }
        
        await userRepository.Create(userToCreate.Value, cancellationToken);

        
        return userToCreate.Value.Id;
    }
}