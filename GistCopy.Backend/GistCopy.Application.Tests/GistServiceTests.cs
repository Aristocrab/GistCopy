using FluentValidation;
using GistCopy.Application.Dto.Gist;
using GistCopy.Application.Services;
using GistCopy.Application.Tests.Common;
using GistCopy.Application.Validators;
using GistCopy.Domain.Exceptions;
using Xunit;

namespace GistCopy.Application.Tests;

public class GistServiceTests
{
    private readonly GistService _gistService;
    
    public GistServiceTests()
    {
        var dbContext = TestHelper.CreateDbContext();
        var validator = new CreateGistDtoValidator();
        _gistService = new GistService(dbContext, validator);
    }

    [Fact]
    public void Cant_get_nonexistent_gist()
    {
        var emptyId = Guid.Empty;

        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await _gistService.GetGistById(emptyId, emptyId);
        });
    }
    
    [Fact]
    public void Non_owner_cant_get_private_gist()
    {
        Assert.ThrowsAsync<ForbiddenException>(async () =>
        {
            await _gistService.GetGistById(TestHelper.PrivateGistId, TestHelper.PublicGistOwnerId);
        });
    }
    
    [Fact]
    public void Cant_create_invalid_gist()
    {
        const string invalidFilename = "imnot a filename.";

        var createGistDto = new CreateGistDto
        {
            UserId = TestHelper.PublicGistOwnerId,
            Description = "Description",
            Filename = invalidFilename,
            Text = "Text"
        };

        Assert.False(CreateGistDtoValidator.BeAValidFilename(invalidFilename));
        Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await _gistService.CreateGist(createGistDto);
        });
    }
    
    [Fact]
    public void Cant_create_empty_gist()
    {
        var createGistDto = new CreateGistDto
        {
            UserId = TestHelper.PublicGistOwnerId,
            Description = "",
            Filename = "",
            Text = ""
        };

        Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await _gistService.CreateGist(createGistDto);
        });
    }
    
    [Fact]
    public void Nonexisting_user_cant_create_gist()
    {
        var createGistDto = new CreateGistDto
        {
            UserId = TestHelper.UnexistingGuid,
            Description = "Description",
            Filename = "a.txt",
            Text = "Text"
        };

        Assert.ThrowsAsync<UnauthorizedException>(async () =>
        {
            await _gistService.CreateGist(createGistDto);
        });
    }
    
    [Fact]
    public void Cant_delete_nonexisting_gist()
    {
        var emptyId = Guid.Empty;

        Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            await _gistService.DeleteGist(emptyId, emptyId);
        });
    }
    
    [Fact]
    public void Nonowner_cant_delete_gist()
    {
        Assert.ThrowsAsync<ForbiddenException>(async () =>
        {
            await _gistService.DeleteGist(TestHelper.PrivateGistId, TestHelper.PublicGistOwnerId);
        });
    }
}