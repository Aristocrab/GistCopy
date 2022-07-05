using System.Security.Claims;
using GistCopy.Domain.Entities;
using GistCopy.Domain.Exceptions;
using GistCopy.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GistCopy.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected UserVm CurrentUser
    {
        get
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity is null) throw new NotFoundException(nameof(User), Guid.Empty);
            
            var userClaims = identity.Claims.ToArray();

            return new UserVm
            {
                Id = Guid.Parse(userClaims.FirstOrDefault(x => x.Type == "Id")!.Value),
                Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value
            };
        }
    }
}