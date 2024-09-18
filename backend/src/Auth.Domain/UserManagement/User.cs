using Auth.Domain.Shared;
using Auth.Domain.UserManagement.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.UserManagement;

public class User : IdentityUser 
{
    private User() { }

    private User(
        FullName fullName,
        IsDeleted isDeleted,
        LastAuthAt lastAuthAt)
    { 
        FullName = fullName;
        IsDeleted = isDeleted;
        LastAuthAt = lastAuthAt;
        RegisterAt = new RegisterAt(DateTime.UtcNow);
    }
    
    public FullName FullName { get; private set; } = default!;
    public RegisterAt RegisterAt { get; private set; } = default!;
    public LastAuthAt LastAuthAt { get; private set; } = default!;
    public IsDeleted IsDeleted { get; private set; } = default!;

    public void SetEmail(Email email) => Email = email.Value;
    public void SetDeletedStatus(IsDeleted status) => IsDeleted = status;

    public static Result<User, Error> Create(
        FullName fullName,
        IsDeleted isDeleted,
        LastAuthAt lastAuthAt,
        Email email)
    {
        var user = new User(
            fullName,
            isDeleted,
            lastAuthAt);
        
        user.SetEmail(email);

        return user;
    }
}