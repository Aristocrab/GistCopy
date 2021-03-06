using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GistCopy.Domain.Entities;
using GistCopy.Domain.Exceptions;
using GistCopy.Application.Dto.Comment;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GistCopy.Application.Services;

public class CommentService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<AddCommentDto> _validator;

    public CommentService(ApplicationDbContext dbContext, IValidator<AddCommentDto> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task<List<GetCommentVm>> GetGistComments(Guid gistId, Guid userId)
    {
        var gist = await _dbContext.Gists.AsNoTracking().Include(g => g.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(gist => gist.Id == gistId);

        if (gist is null)
        {
            throw new EntityNotFoundException(nameof(Gist), gistId);
        }

        if (gist.Private && gist.User.Id != userId)
        {
            throw new ForbiddenException(nameof(Comment), gistId, userId);
        }

        var config = new TypeAdapterConfig();
        config.NewConfig<Comment, GetCommentVm>()
            .Map(dest => dest.TimeCreated,
            src => src.TimeCreated.ToShortTimeString() + " " + src.TimeCreated.ToShortDateString());
        
        var comments = gist.Comments.AsQueryable()
            .OrderBy(c => c.TimeCreated)
            .ProjectToType<GetCommentVm>(config)
            .ToList();
        return comments;
    }

    public async Task<Guid> AddComment(AddCommentDto addCommentDto)
    {
        var result = await _validator.ValidateAsync(addCommentDto);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        
        var gist = await _dbContext.Gists.FirstOrDefaultAsync(gist => gist.Id == addCommentDto.GistId);

        if (gist is null)
        {
            throw new EntityNotFoundException(nameof(Gist), addCommentDto.GistId);
        }

        if (gist.Private && addCommentDto.GistId != addCommentDto.UserId)
        {
            throw new ForbiddenException(nameof(Gist), addCommentDto.GistId, addCommentDto.UserId);
        }
        
        var comment = addCommentDto.Adapt<Comment>();
        
        var user = _dbContext.Users.First(x => x.Id == addCommentDto.UserId);
        if (user is null)
        {
            throw new UnauthorizedException();
        }

        comment.User = user;
        gist.Comments = new List<Comment>
        {
            comment
        };

        await _dbContext.SaveChangesAsync();
        return comment.Id;
    }

    public async Task DeleteComment(Guid id, Guid userId)
    {
        var comment = await _dbContext.Comments.Include(x=>x.User)
            .FirstOrDefaultAsync(gist => gist.Id == id);
        
        if (comment is null)
        {
            throw new EntityNotFoundException(nameof(Comment), id);
        }

        if (comment.User.Id != userId)
        {
            throw new ForbiddenException(nameof(Comment), id, userId);
        }
        
        _dbContext.Remove(comment);
        await _dbContext.SaveChangesAsync();
    }
}