using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TagsInPlaylistRepository : BaseRepository<DTO.TagsInPlaylist, Domain.TagsInPlaylist>, ITagsInPlaylistRepository
{
    private readonly TagsInPlaylistUOWMapper _iuowMapper = new TagsInPlaylistUOWMapper();
    public TagsInPlaylistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TagsInPlaylistUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.TagsInPlaylist>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(t => t.Playlist)
            .Include(t => t.Tag)
            .ToListAsync())
            .Select(e => _iuowMapper.Map(e)!);
    }

    public override async Task<DTO.TagsInPlaylist?> FindAsync(Guid id, string? userId)
    {
        var entity = await GetQuery(userId)
            .Include(r => r.Tag)
            .Include(r => r.Playlist)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _iuowMapper.Map(entity);
    }
}