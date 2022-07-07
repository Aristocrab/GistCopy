using System;
using GistCopy.Application.Dto.User;

namespace GistCopy.Application.Dto.Gist;

public class GetGistVm
{
    public Guid Id { get; set; }
    public UserDto User { get; set; } = null!;
    public string TimeCreated { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Filename { get; set; } = null!;
    public string Text { get; set; } = null!;
    public bool Private { get; set; }
}