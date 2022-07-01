using System;

namespace GistCopy.Domain.Entities.Base;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTime TimeCreated { get; }

    protected EntityBase()
    {
        TimeCreated = DateTime.Now;
    }
}