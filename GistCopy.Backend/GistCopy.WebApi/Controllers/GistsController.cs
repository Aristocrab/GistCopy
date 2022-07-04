using GistCopy.Application.Dto;
using GistCopy.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GistCopy.WebApi.Controllers;

public class GistsController : BaseController
{
    private readonly GistsService _gistsService;

    public GistsController(GistsService gistsService)
    {
        _gistsService = gistsService;
    }

    [HttpGet]
    public ActionResult<List<GetGistDto>> GetAll()
    {
        return Ok(_gistsService.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetGistDto>> GetGist(Guid id)
    {
        return Ok(await _gistsService.GetGistById(id));
    }

    [HttpPost("new")]
    public async Task<ActionResult<Guid>> CreateGist(CreateGistDto createGistDto)
    {
        var newGist = await _gistsService.CreateGist(createGistDto);
        return Created(nameof(GetAll), newGist);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteGist(Guid id)
    {
        await _gistsService.DeleteGist(id);
        return NoContent();
    }
}