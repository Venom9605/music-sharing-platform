using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PlaylistRepository : BaseRepository<DTO.Playlist, Domain.Playlist>, IPlaylistRepository
{
    private readonly PlaylistMapper _mapper = new PlaylistMapper();
    public PlaylistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new PlaylistMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.Playlist>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(p => p.User)
            .ToListAsync())
            .Select(e => _mapper.Map(e)!);
    }
}