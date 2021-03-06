using System;
using FluentValidation;
using GistCopy.Application.Dto.Gist;

namespace GistCopy.Application.Validators;

public class CreateGistDtoValidator: AbstractValidator<CreateGistDto>
{
    public CreateGistDtoValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.Description).MaximumLength(256);
        RuleFor(x => x.Filename).Must(BeAValidFilename).WithMessage("Filename must be a valid filename")
            .MaximumLength(128);
        RuleFor(x => x.Text).NotEmpty().MaximumLength(3000);
    }
    
    public static bool BeAValidFilename(string filename)
    {
        // Filenames: a.txt || .gitignore
        return filename.Contains('.') && 
               !filename.Contains(' ') &&
               filename.IndexOf('.') != filename.Length - 1;
    }
}