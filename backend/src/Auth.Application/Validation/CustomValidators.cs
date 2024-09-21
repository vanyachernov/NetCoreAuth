using Auth.Domain.Shared;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Auth.Application.Validation;

public static class CustomValidators
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((valueObject, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(valueObject);

            if (result.IsSuccess)
            {
                return;
            }
            
            context.AddFailure(result.Error.Serialize());
        });
    }
}