using Auth.Application.Validation;
using Auth.Domain.UserManagement.ValueObjects;
using FluentValidation;

namespace Auth.Application.Users.AuthenticateUser;

public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
{
    public AuthenticateUserRequestValidator()
    {
        RuleFor(c => c.Password).NotEmpty();
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
    }
}