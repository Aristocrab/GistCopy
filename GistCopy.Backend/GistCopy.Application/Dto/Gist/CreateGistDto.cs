using System;

namespace GistCopy.Application.Dto.Gist;

public class CreateGistDto
{
    public Guid UserId { get; set; }
    public string Description { get; set; } = null!;
    public string Filename { get; set; } = null!;
    public string Text { get; set; } = null!;
    public bool Private { get; set; }
}