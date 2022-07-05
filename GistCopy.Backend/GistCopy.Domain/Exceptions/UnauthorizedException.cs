using System;

namespace GistCopy.Domain.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() 
        : base("Request caller is not authorized") { }
}