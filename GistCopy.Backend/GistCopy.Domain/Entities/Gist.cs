using System.Collections.Generic;
using GistCopy.Domain.Entities.Base;

namespace GistCopy.Domain.Entities;

public class Gist : EntityBase
{
    public string Description { get; set; } = null!;
    public string Filename { get; set; } = null!;
    public string Text { get; set; } = null!;
    public bool Private { get; set; }

    public User User { get; set; } = null!;
    public List<Comment> Comments { get; set; } = null!;
}