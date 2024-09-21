using Auth.Application.Validation;
using Auth.Domain.UserManagement.ValueObjects;
using FluentValidation;

namespace Auth.Application.Users.CreateUser;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(c => c.FullNameDto)
            .MustBeValueObject(f => 
                FullName.Create(f.FirstName, f.LastName));
        
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
    }
}