using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GistCopy.Application.Dto;
using GistCopy.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GistCopy.WebApi.Controllers;

public class CommentsController : BaseController
{
    private readonly CommentsService _commentsService;

    public CommentsController(CommentsService commentsService)
    {
        _commentsService = commentsService;
    }   
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<List<GetCommentDto>>> GetGistComments(Guid id)
    {
        return Ok(await _commentsService.GetGistComments(id));
    }

    [HttpPost("add")]
    public async Task<ActionResult<Guid>> AddComment(AddCommentDto addCommentDto)
    {
        var newGist = await _commentsService.AddComment(addCommentDto);
        return Created(nameof(GetGistComments), newGist);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteComment(Guid id)
    {
        await _commentsService.DeleteComment(id);
        return NoContent();
    }
}