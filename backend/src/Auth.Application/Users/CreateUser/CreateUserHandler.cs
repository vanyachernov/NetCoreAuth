using Auth.Domain.Shared.Interfaces;
using Auth.Domain.UserManagement;
using Auth.Domain.UserManagement.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Users.CreateUser;

public class CreateUserHandler
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserHandler(
        IUsersRepository usersRepository,
        IPasswordHasher passwordHasher)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
    }
    
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

        var hashedPassword = _passwordHasher.Generate(request.Password);
        
        var existingUser = await _usersRepository.GetByEmail(
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
        
        await _usersRepository.Register(userToCreate.Value, hashedPassword, cancellationToken);
        
        return userToCreate.Value.Id;
    }
}