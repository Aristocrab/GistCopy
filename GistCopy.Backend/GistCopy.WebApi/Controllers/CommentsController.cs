using GistCopy.Application.Dto;
using GistCopy.Application.Services;
using GistCopy.WebApi.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GistCopy.WebApi.Controllers;

public class CommentsController : BaseController
{
    private readonly CommentService _commentService;

    public CommentsController(CommentService commentService)
    {
        _commentService = commentService;
    }   
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<List<GetCommentDto>>> GetGistComments(Guid id)
    {
        return Ok(await _commentService.GetGistComments(id));
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<ActionResult<Guid>> AddComment(AddCommentVm addCommentVm)
    {
        var addCommentDto = addCommentVm.Adapt<AddCommentDto>();
        addCommentDto.UserId = CurrentUser.Id;

        var newComment = await _commentService.AddComment(addCommentDto);
        return Created(nameof(GetGistComments), newComment);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteComment(Guid id)
    {
        await _commentService.DeleteComment(id, CurrentUser.Id);
        return NoContent();
    }
}