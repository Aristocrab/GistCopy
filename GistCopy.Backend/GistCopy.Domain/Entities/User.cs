using System.Collections.Generic;
using GistCopy.Domain.Entities.Base;

namespace GistCopy.Domain.Entities;

public class User : EntityBase
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    public List<Gist> Gists { get; set; } = null!;
    public List<Comment> Comments { get; set; } = null!;
}