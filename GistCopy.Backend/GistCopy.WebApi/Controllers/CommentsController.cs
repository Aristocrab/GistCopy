using GistCopy.Application.Dto.Comment;
using GistCopy.Application.Services;
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
    public async Task<ActionResult<List<GetCommentVm>>> GetGistComments(Guid id)
    {
        return Ok(await _commentService.GetGistComments(id, CurrentUser.Id));
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<ActionResult<Guid>> AddComment(AddCommentDto addCommentDto)
    {
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