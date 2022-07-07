using System;

namespace GistCopy.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entity, Guid id) 
        : base($"Entity '{entity}' with Id: {id} was not found") { }
}