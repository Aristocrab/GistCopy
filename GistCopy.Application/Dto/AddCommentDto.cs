using System;

namespace GistCopy.Application.Dto;

public class AddCommentDto
{
    public Guid GistId { get; set; }
    public string Text { get; set; }
}