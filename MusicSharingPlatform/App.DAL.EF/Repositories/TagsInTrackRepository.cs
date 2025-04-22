using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TagsInTrackRepository : BaseRepository<DTO.TagsInTrack, Domain.TagsInTrack>, ITagsInTrackRepository
{
    private readonly TagsInTrackMapper _mapper = new TagsInTrackMapper();
    public TagsInTrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TagsInTrackMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.TagsInTrack>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(t => t.Track)
            .Include(t => t.Tag)
            .ToListAsync())
            .Select(e => _mapper.Map(e)!);
    }
}