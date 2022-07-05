using System;
using GistCopy.Application.Dto.User;

namespace GistCopy.Application.Dto.Gist;

public class GetGistDto
{
    public Guid Id { get; set; }
    public UserDto User { get; set; } = null!;
    public DateTime TimeCreated { get; set; }
    public string Description { get; set; } = null!;
    public string Filename { get; set; } = null!;
    public string Text { get; set; } = null!;
}