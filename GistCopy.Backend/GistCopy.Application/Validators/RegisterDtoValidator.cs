using FluentValidation;
using GistCopy.Application.Dto.User;

namespace GistCopy.Application.Validators;

public class RegisterDtoValidator: AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().Matches("^[A-Za-z0-9_]+$")
            .WithMessage("Username can contain letters, numbers or underlines only")
            .MaximumLength(32);
        RuleFor(x => x.Password).NotEmpty().Matches("^[A-Za-z0-9_]+$")
            .WithMessage("Password can contain letters, numbers or underlines only")
            .MaximumLength(64);
    }
}