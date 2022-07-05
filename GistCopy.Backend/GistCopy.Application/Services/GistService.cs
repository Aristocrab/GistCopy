using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GistCopy.Domain.Entities;
using GistCopy.Domain.Exceptions;
using GistCopy.Application.Dto;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GistCopy.Application.Services;

public class GistService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<Gist> _validator;

    public GistService(ApplicationDbContext dbContext, IValidator<Gist> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public List<GetGistDto> GetAll()
    {
        // First 8 lines of text + ...
        var config = new TypeAdapterConfig();
        config.ForType<Gist, GetGistDto>().Map(
            dest => dest.Text,
            src => TrimLongText(src.Text, 8));
        
        return _dbContext.Gists
            .OrderBy(g => g.TimeCreated)
            .ProjectToType<GetGistDto>(config)
            .ToList();
    }

    public List<GetGistDto> GetUserGists(Guid userId)
    {
        var config = new TypeAdapterConfig();
        config.ForType<Gist, GetGistDto>().Map(
            dest => dest.Text,
            src => TrimLongText(src.Text, 8));
        
        return _dbContext.Gists
            .OrderBy(g => g.TimeCreated)
            .Include(x => x.User)
            .Where(x => x.User.Id == userId)
            .ProjectToType<GetGistDto>(config)
            .ToList();
    }

    private static string TrimLongText(string text, int maxLength)
    {
        if (text.Split('\n').Length > maxLength)
        {
            var lines = text.Split('\n').Take(8);
            return string.Join('\n', lines) + "...";
        }

        return text;
    }

    public async Task<GetGistDto> GetGistById(Guid id)
    {
        var gist = await _dbContext.Gists.Include(g => g.User).AsNoTracking()
            .FirstOrDefaultAsync(gist => gist.Id == id);

        if (gist is null)
        {
            throw new NotFoundException(nameof(Gist), id);
        }
        
        return gist.Adapt<GetGistDto>();
    }

    public async Task<Guid> CreateGist(CreateGistDto createGistDto)
    {
        var gist = createGistDto.Adapt<Gist>();
        
        var result = await _validator.ValidateAsync(gist);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        var user = _dbContext.Users.First(x => x.Id == createGistDto.UserId);
        
        if (user is null)
        {
            throw new NotFoundException(nameof(User), createGistDto.UserId);
        }

        gist.User = user;
        _dbContext.Gists.Add(gist);
        await _dbContext.SaveChangesAsync();

        return gist.Id;
    }

    public async Task DeleteGist(Guid id, Guid userId)
    {
        var gist = await _dbContext.Gists.Include(x => x.User)
            .FirstOrDefaultAsync(gist => gist.Id == id);
        if (gist is null)
        {
            throw new NotFoundException(nameof(Gist), id);
        }

        if (gist.User.Id != userId)
        {
            throw new ForbiddenException(nameof(Gist), id, userId);
        }
        
        _dbContext.Gists.Remove(gist);
        await _dbContext.SaveChangesAsync();
    }
}