using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TagsInTrackRepository : BaseRepository<DTO.TagsInTrack, Domain.TagsInTrack>, ITagsInTrackRepository
{
    private readonly TagsInTrackUOWMapper _iuowMapper = new TagsInTrackUOWMapper();
    public TagsInTrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TagsInTrackUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.TagsInTrack>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(t => t.Track)
            .Include(t => t.Tag)
            .ToListAsync())
            .Select(e => _iuowMapper.Map(e)!);
    }
    
    public override async Task<DTO.TagsInTrack?> FindAsync(Guid id, string? userId)
    {
        var entity = await GetQuery(userId)
            .Include(r => r.Track)
            .Include(r => r.Tag)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _iuowMapper.Map(entity);
    }
}