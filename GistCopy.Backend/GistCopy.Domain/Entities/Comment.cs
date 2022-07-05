using GistCopy.Domain.Entities.Base;

namespace GistCopy.Domain.Entities;

public class Comment : EntityBase
{
    public string Text { get; set; }

    public User User { get; set; }
    public Gist Gist { get; set; }
}