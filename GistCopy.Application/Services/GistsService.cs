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

public class GistsService
{
    private readonly ApplicationDbContext _dbContext;

    public GistsService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public List<GetGistDto> GetAll()
    {
        return _dbContext.Gists.ProjectToType<GetGistDto>().ToList();
    }

    public async Task<GetGistDto> GetGistById(Guid id)
    {
        var gist = await _dbContext.Gists.AsNoTracking().FirstOrDefaultAsync(gist => gist.Id == id);

        if (gist is not null)
        {
            return gist.Adapt<GetGistDto>();
        }

        throw new NotFoundException(nameof(Gist), id);
    }

    public async Task<Guid> CreateGist(CreateGistDto createGistDto)
    {
        var gist = createGistDto.Adapt<Gist>();

        _dbContext.Gists.Add(gist);
        await _dbContext.SaveChangesAsync();

        return gist.Id;
    }

    public async Task DeleteGist(Guid id)
    {
        var gist = await _dbContext.Gists.FirstOrDefaultAsync(gist => gist.Id == id);
        if (gist is not null)
        {
            _dbContext.Gists.Remove(gist);
            await _dbContext.SaveChangesAsync();
        }
    }
}