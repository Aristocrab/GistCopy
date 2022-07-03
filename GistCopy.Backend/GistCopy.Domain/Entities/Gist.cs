using System.Collections.Generic;
using GistCopy.Domain.Entities.Base;

namespace GistCopy.Domain.Entities;

public class Gist : EntityBase
{
    public string Description { get; set; }
    public string Filename { get; set; }
    public string Text { get; set; }

    public List<Comment> Comments { get; set; }
}