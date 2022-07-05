using System;
using GistCopy.Application.Dto.User;

namespace GistCopy.Application.Dto;

public class GetCommentDto
{
    public Guid Id { get; set; }
    public UserDto User { get; set; }
    public DateTime TimeCreated { get; set; }
    public string Text { get; set; }
}