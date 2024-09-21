using Auth.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Auth.Domain.UserManagement.ValueObjects;

public record FullName
{
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;

    public static Result<FullName, Error> Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > Constants.MAX_LOW_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("FirstName");
        }
        
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > Constants.MAX_LOW_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("LastName");
        }

        return new FullName(firstName, lastName);
    }
}