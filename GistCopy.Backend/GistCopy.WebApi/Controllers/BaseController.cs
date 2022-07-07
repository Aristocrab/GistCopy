using System.Security.Claims;
using GistCopy.Application.Dto.User;
using GistCopy.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GistCopy.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected UserDto CurrentUser
    {
        get
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity is null) throw new UserNotFoundException();
            
            var userClaims = identity.Claims.ToArray();
            if (!userClaims.Any())
            {
                return new UserDto
                {
                    Id = Guid.Empty,
                    Username = string.Empty
                };
            }

            return new UserDto
            {
                Id = Guid.Parse(userClaims.FirstOrDefault(x => x.Type == "Id")!.Value),
                Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value
            };
        }
    }
}