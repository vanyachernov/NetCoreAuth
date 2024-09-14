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

    public static Result<User> Create(
        FullName fullName,
        IsDeleted isDeleted,
        LastAuthAt lastAuthAt)
    {
        return new User(
            fullName,
            isDeleted,
            lastAuthAt);
    }
}