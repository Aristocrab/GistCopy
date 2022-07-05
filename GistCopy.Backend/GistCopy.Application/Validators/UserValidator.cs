using FluentValidation;
using GistCopy.Domain.Entities;

namespace GistCopy.Application.Validators;

public class UserValidator: AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(32);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(64);
    }
}