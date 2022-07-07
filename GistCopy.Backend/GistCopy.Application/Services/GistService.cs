using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GistCopy.Domain.Entities;
using GistCopy.Domain.Exceptions;
using GistCopy.Application.Dto.Gist;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GistCopy.Application.Services;

public class GistService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<CreateGistDto> _validator;
    private readonly TypeAdapterConfig _gistPreviewConfig;

    public GistService(ApplicationDbContext dbContext, IValidator<CreateGistDto> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
        
        _gistPreviewConfig = new TypeAdapterConfig();
        _gistPreviewConfig.ForType<Gist, GetGistVm>()
            .Map(
            dest => dest.Text,
            src => TrimLongText(src.Text, 8))
            .Map(dest => dest.TimeCreated,
            src => src.TimeCreated.ToShortTimeString() + " " + src.TimeCreated.ToShortDateString());
    }
    
    public List<GetGistVm> GetAll()
    {
        // First 8 lines of text + ...
        return _dbContext.Gists
            .Where(g => !g.Private)
            .OrderBy(g => g.TimeCreated)
            .ProjectToType<GetGistVm>(_gistPreviewConfig)
            .ToList();
    }

    public List<GetGistVm> GetUserGists(Guid userId)
    {
        // First 8 lines of text + ...
        return _dbContext.Gists
            .OrderBy(g => g.TimeCreated)
            .Include(x => x.User)
            .Where(x => x.User.Id == userId)
            .ProjectToType<GetGistVm>(_gistPreviewConfig)
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

    public async Task<GetGistVm> GetGistById(Guid id, Guid userId)
    {
        var gist = await _dbContext.Gists.Include(g => g.User).AsNoTracking()
            .FirstOrDefaultAsync(gist => gist.Id == id);

        if (gist is null)
        {
            throw new EntityNotFoundException(nameof(Gist), id);
        }

        if (gist.Private && gist.User.Id != userId)
        {
            throw new ForbiddenException(nameof(Gist), gist.Id, userId);
        }

        var config = new TypeAdapterConfig();
        config.NewConfig<Gist, GetGistVm>()
            .Map(dest => dest.TimeCreated,
            src => src.TimeCreated.ToShortTimeString() + " " + src.TimeCreated.ToShortDateString());
        
        return gist.Adapt<GetGistVm>(config);
    }

    public async Task<Guid> CreateGist(CreateGistDto createGistDto)
    {
        var result = await _validator.ValidateAsync(createGistDto);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        
        var gist = createGistDto.Adapt<Gist>();

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == createGistDto.UserId);
        if (user is null)
        {
            throw new UnauthorizedException();
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
            throw new EntityNotFoundException(nameof(Gist), id);
        }

        if (gist.User.Id != userId)
        {
            throw new ForbiddenException(nameof(Gist), id, userId);
        }
        
        _dbContext.Gists.Remove(gist);
        await _dbContext.SaveChangesAsync();
    }
}