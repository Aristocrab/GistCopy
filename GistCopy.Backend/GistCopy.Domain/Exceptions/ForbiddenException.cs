using System;

namespace GistCopy.Domain.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string entity, Guid id, Guid userId) 
        : base($"User '{userId}' does not own entity '{entity}' with Id: {id}") { }
}