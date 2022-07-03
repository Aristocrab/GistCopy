using System;

namespace GistCopy.Application.Dto;

public class GetCommentDto
{
    public Guid Id { get; set; }
    public DateTime TimeCreated { get; set; }
    public string Text { get; set; }
}