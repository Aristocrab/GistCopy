using FluentValidation;
using GistCopy.Domain.Entities;

namespace GistCopy.Application.Validators;

public class CommentValidator: AbstractValidator<Comment>
{
    public CommentValidator()
    {
        RuleFor(x => x.Text).NotEmpty().WithMessage("Please specify a comment's text").MaximumLength(512);
    }
}