using System;

namespace GistCopy.Domain.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() 
        : base("Access denied") { }
}