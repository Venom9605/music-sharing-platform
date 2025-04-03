using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TagsInPlaylistRepository : BaseRepository<TagsInPlaylist>, ITagsInPlaylistRepository
{
    public TagsInPlaylistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<TagsInPlaylist>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .Include(t => t.Playlist)
            .Include(t => t.Tag)
            .ToListAsync();
    }
}