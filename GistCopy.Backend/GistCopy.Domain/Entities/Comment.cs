using GistCopy.Domain.Entities.Base;

namespace GistCopy.Domain.Entities;

public class Comment : EntityBase
{
    public string Text { get; set; } = null!;

    public User User { get; set; } = null!;
    public Gist Gist { get; set; } = null!;
}