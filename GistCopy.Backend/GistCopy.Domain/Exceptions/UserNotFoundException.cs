using System;

namespace GistCopy.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() 
        : base("User with provided credentials was not found") { }
}