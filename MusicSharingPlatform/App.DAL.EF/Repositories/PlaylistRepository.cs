using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PlaylistRepository : BaseRepository<DTO.Playlist, Domain.Playlist>, IPlaylistRepository
{
    private readonly PlaylistUOWMapper _iuowMapper = new PlaylistUOWMapper();
    public PlaylistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new PlaylistUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.Playlist>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(p => p.User)
            .ToListAsync())
            .Select(e => _iuowMapper.Map(e)!);
    }
}