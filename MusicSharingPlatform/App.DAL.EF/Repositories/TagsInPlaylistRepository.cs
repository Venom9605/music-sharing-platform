using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TagsInPlaylistRepository : BaseRepository<DTO.TagsInPlaylist, Domain.TagsInPlaylist>, ITagsInPlaylistRepository
{
    private readonly TagsInPlaylistMapper _mapper = new TagsInPlaylistMapper();
    public TagsInPlaylistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TagsInPlaylistMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.TagsInPlaylist>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(t => t.Playlist)
            .Include(t => t.Tag)
            .ToListAsync())
            .Select(e => _mapper.Map(e)!);
    }
}