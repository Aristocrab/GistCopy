using System;

namespace GistCopy.Application.Dto;

public class CreateGistDto
{
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public string Filename { get; set; }
    public string Text { get; set; }
}