using System;
using GistCopy.Application.Dto.User;

namespace GistCopy.Application.Dto.Comment;

public class GetCommentVm
{
    public Guid Id { get; set; }
    public UserDto User { get; set; } = null!;
    public string TimeCreated { get; set; } = null!;
    public string Text { get; set; } = null!;
}