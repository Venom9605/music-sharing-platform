using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TagsInTrackRepository : BaseRepository<TagsInTrack>, ITagsInTrackRepository
{
    public TagsInTrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<TagsInTrack>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .Include(t => t.Track)
            .Include(t => t.Tag)
            .ToListAsync();
    }
}