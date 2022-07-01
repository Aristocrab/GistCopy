using FluentValidation;
using GistCopy.Domain.Entities;

namespace GistCopy.Application.Validators;

public class GistValidator: AbstractValidator<Gist>
{
    public GistValidator()
    {
        RuleFor(x => x.Description).MaximumLength(256);
        RuleFor(x => x.Filename).Must(BeAValidFilename).WithMessage("Filename must be a valid filename")
            .MaximumLength(128);
        RuleFor(x => x.Text).NotEmpty().MaximumLength(3000);
    }
    
    private bool BeAValidFilename(string filename)
    {
        // Filenames: a.txt || .gitignore
        return filename.Contains('.') && 
               !filename.Contains(' ') &&
               filename.IndexOf('.') != filename.Length - 1;
    }
}