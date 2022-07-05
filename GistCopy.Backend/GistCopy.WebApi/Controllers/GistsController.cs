using GistCopy.Application.Dto;
using GistCopy.Application.Dto.Gist;
using GistCopy.Application.Services;
using GistCopy.WebApi.Models;
using Mapster;
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
    public ActionResult<List<GetGistDto>> GetAll()
    {
        return Ok(_gistService.GetAll());
    }
    
    [Authorize]
    [HttpGet("my")]
    public ActionResult<List<GetGistDto>> GetUserGists()
    {
        return Ok(_gistService.GetUserGists(CurrentUser.Id));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetGistDto>> GetGist(Guid id)
    {
        return Ok(await _gistService.GetGistById(id));
    }

    [Authorize]
    [HttpPost("new")]
    public async Task<ActionResult<Guid>> CreateGist(CreateGistVm createGistVm)
    {
        var createGistDto = createGistVm.Adapt<CreateGistDto>();
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