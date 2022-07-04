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
    private readonly IValidator<Gist> _validator;

    public GistsService(ApplicationDbContext dbContext, IValidator<Gist> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
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
        
        var result = await _validator.ValidateAsync(gist);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

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