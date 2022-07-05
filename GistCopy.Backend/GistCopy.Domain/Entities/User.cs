using System.Collections.Generic;
using GistCopy.Domain.Entities.Base;

namespace GistCopy.Domain.Entities;

public class User : EntityBase
{
    public string Username { get; set; }
    public string Password { get; set; }

    public List<Gist> Gists { get; set; }
    public List<Comment> Comments { get; set; }
}