using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MoodsInPlaylistRepository : BaseRepository<MoodsInPlaylist>, IMoodsInPlaylistRepository
{
    public MoodsInPlaylistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<MoodsInPlaylist>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .Include(m => m.Mood)
            .Include(m => m.Playlist)
            .ToListAsync();
    }
}