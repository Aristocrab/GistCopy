using GistCopy.Application.Dto.Gist;
using GistCopy.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GistCopy.WebApi.Controllers;

public class GistsController : BaseController
{
    private readonly GistService _gistService;

    public GistsController(GistService gistService)
    {
        _gistService = gistService;
    }

    [HttpGet]
    public ActionResult<List<GetGistVm>> GetAll()
    {
        return Ok(_gistService.GetAll());
    }
    
    [Authorize]
    [HttpGet("my")]
    public ActionResult<List<GetGistVm>> GetUserGists()
    {
        return Ok(_gistService.GetUserGists(CurrentUser.Id));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetGistVm>> GetGist(Guid id)
    {
        return Ok(await _gistService.GetGistById(id, CurrentUser.Id));
    }

    [Authorize]
    [HttpPost("new")]
    public async Task<ActionResult<Guid>> CreateGist(CreateGistDto createGistDto)
    {
        createGistDto.UserId = CurrentUser.Id;
        
        var newGist = await _gistService.CreateGist(createGistDto);
        return Created(nameof(GetAll), newGist);
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteGist(Guid id)
    {
        await _gistService.DeleteGist(id, CurrentUser.Id);
        return NoContent();
    }
}