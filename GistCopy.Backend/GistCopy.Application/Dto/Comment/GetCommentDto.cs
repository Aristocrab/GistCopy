using System;
using GistCopy.Application.Dto.User;

namespace GistCopy.Application.Dto.Comment;

public class GetCommentDto
{
    public Guid Id { get; set; }
    public UserDto User { get; set; } = null!;
    public string TimeCreated { get; set; }
    public string Text { get; set; } = null!;
}