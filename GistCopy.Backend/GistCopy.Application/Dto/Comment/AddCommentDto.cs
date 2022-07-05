using System;

namespace GistCopy.Application.Dto;

public class AddCommentDto
{
    public Guid UserId { get; set; }
    public Guid GistId { get; set; }
    public string Text { get; set; }
}