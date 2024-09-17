using Auth.Domain.Shared;
using Auth.Domain.Shared.Interfaces;
using Auth.Domain.UserManagement;
using Auth.Domain.UserManagement.ValueObjects;
using CSharpFunctionalExtensions;

namespace Auth.Application.Users.CreateUser;

public class CreateUserHandler(IUsersRepository usersRepository)
{
    public async Task<Result<Guid, Error>> Handle(
        CreateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var fullName = FullName.Create(
            request.FullNameDto.FirstName, 
            request.FullNameDto.LastName).Value;
        var isDeletedStatus = new IsDeleted(false);
        var lastAuthAt = new LastAuthAt(DateTime.UtcNow);
        var email = Email.Create(request.Email).Value;
        
        var emailParts = email.Value.Split('@');
        var username = emailParts[0];
        
        var userToCreate = User.Create(
            fullName,
            isDeletedStatus,
            lastAuthAt,
            email);
        
        if (userToCreate.IsFailure)
        {
            return userToCreate.Error;
        }

        userToCreate.Value.UserName = username;
        
        await usersRepository.Register(userToCreate.Value, request.Password, cancellationToken);

        Guid.TryParse(userToCreate.Value.Id, out var userToCreateIdentifier);

        return userToCreateIdentifier;
    }
}