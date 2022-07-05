using System;
using FluentValidation;
using GistCopy.Application.Dto.Comment;

namespace GistCopy.Application.Validators;

public class AddCommentDtoValidator: AbstractValidator<AddCommentDto>
{
    public AddCommentDtoValidator()
    {
        RuleFor(x => x.GistId).NotEqual(Guid.Empty);
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.Text).NotEmpty().WithMessage("Please specify a comment's text")
            .MaximumLength(512);
    }
}