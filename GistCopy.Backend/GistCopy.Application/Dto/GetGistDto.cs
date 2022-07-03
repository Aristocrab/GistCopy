using System;

namespace GistCopy.Application.Dto;

public class GetGistDto
{
    public Guid Id { get; set; }
    public DateTime TimeCreated { get; set; }
    public string Description { get; set; }
    public string Filename { get; set; }
    public string Text { get; set; }
}