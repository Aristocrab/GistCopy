using FluentValidation;
using GistCopy.Application.Dto.Comment;
using GistCopy.Application.Services;
using GistCopy.Application.Tests.Common;
using GistCopy.Application.Validators;
using GistCopy.Domain.Exceptions;
using Xunit;

namespace GistCopy.Application.Tests;

public class CommentServiceTests
{
    private readonly CommentService _commentService;
    
    public CommentServiceTests()
    {
        var dbContext = TestHelper.CreateDbContext();
        var validator = new AddCommentDtoValidator();
        _commentService = new CommentService(dbContext, validator);
    }

    [Fact]
    public void Cant_get_comments_of_nonexistent_gist()
    {
        var emptyId = Guid.Empty;

        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await _commentService.GetGistComments(emptyId, emptyId);
        });
    }
    
    [Fact]
    public void Nonowner_cant_get_comments_of_private_gist()
    {
        Assert.ThrowsAsync<ForbiddenException>(async () =>
        {
            await _commentService.GetGistComments(TestHelper.PrivateGistId, 
                TestHelper.PublicGistOwnerId);
        });
    }
    
    [Fact]
    public void Cant_add_empty_comment()
    {
        var addCommentDto = new AddCommentDto
        {
            UserId = TestHelper.PublicGistOwnerId,
            GistId = TestHelper.PublicGistId,
            Text = ""
        };
        
        Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await _commentService.AddComment(addCommentDto);
        });
    }
    
    [Fact]
    public void Cant_add_comment_to_unexisting_gist()
    {
        var addCommentDto = new AddCommentDto
        {
            UserId = TestHelper.PublicGistOwnerId,
            GistId = TestHelper.UnexistingGuid,
            Text = "Text"
        };
        
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await _commentService.AddComment(addCommentDto);
        });
    }
    
    [Fact]
    public void Nonowner_cant_add_comment_to_private_gist()
    {
        var addCommentDto = new AddCommentDto
        {
            UserId = TestHelper.PublicGistOwnerId,
            GistId = TestHelper.PrivateGistId,
            Text = "Your private gist is cool!"
        };
        
        Assert.ThrowsAsync<ForbiddenException>(async () =>
        {
            await _commentService.AddComment(addCommentDto);
        });
    }
    
    [Fact]
    public void Nonexisting_user_cant_add_comment()
    {
        var addCommentDto = new AddCommentDto
        {
            UserId = Guid.Parse("999836B4-C183-4267-899B-6BBB0B72411B"),
            GistId = TestHelper.PublicGistId,
            Text = "Text"
        };
        
        Assert.ThrowsAsync<UnauthorizedException>(async () =>
        {
            await _commentService.AddComment(addCommentDto);
        });
    }
    
    [Fact]
    public void Cant_delete_nonexisting_comment()
    {
        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await _commentService.DeleteComment(TestHelper.UnexistingGuid, TestHelper.UnexistingGuid);
        });
    }
    
    [Fact]
    public void Nonowner_cant_delete_comment()
    {
        Assert.ThrowsAsync<ForbiddenException>(async () =>
        {
            await _commentService.DeleteComment(Guid.Parse("B5B74046-306E-4113-9581-AA1E76854C23"), TestHelper.PublicGistOwnerId);
        });
    }
}