using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using GistCopy.Domain.Entities;
using GistCopy.Domain.Exceptions;
using GistCopy.Application.Dto;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GistCopy.Application.Services;

public class CommentsService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<Comment> _validator;

    public CommentsService(ApplicationDbContext dbContext, IValidator<Comment> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task<List<GetCommentDto>> GetGistComments(Guid gistId)
    {
        var gist = await _dbContext.Gists.AsNoTracking().Include(g => g.Comments)
            .FirstOrDefaultAsync(gist => gist.Id == gistId);

        if (gist is not null)
        {
            return gist.Comments.Adapt<List<GetCommentDto>>();
        }

        throw new NotFoundException(nameof(Gist), gistId);
    }

    public async Task<Guid> AddComment(AddCommentDto addCommentDto)
    {
        var gist = await _dbContext.Gists.FirstOrDefaultAsync(gist => gist.Id == addCommentDto.GistId);

        if (gist is not null)
        {
            var comment = addCommentDto.Adapt<Comment>();
            var result = await _validator.ValidateAsync(comment);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            
            gist.Comments = new List<Comment>
            {
                comment
            };

            await _dbContext.SaveChangesAsync();
            return comment.Id;
        }

        throw new NotFoundException(nameof(Gist), addCommentDto.GistId);
    }

    public async Task DeleteComment(Guid id)
    {
        var comment = await _dbContext.Comments.FirstOrDefaultAsync(gist => gist.Id == id);
        
        if (comment is not null)
        {
            _dbContext.Remove(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
}