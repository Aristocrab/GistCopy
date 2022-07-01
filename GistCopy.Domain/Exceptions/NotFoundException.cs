using System;

namespace GistCopy.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entity, Guid id) 
        : base($"Entity '{entity}' with Id: {id} was not found") { }
}