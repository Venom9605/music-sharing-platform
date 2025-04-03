using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TrackInPlaylistRepository : BaseRepository<TrackInPlaylist>, ITrackInPlaylistRepository
{
    public TrackInPlaylistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<TrackInPlaylist>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .Include(t => t.Track)
            .Include(t => t.Playlist)
            .ToListAsync();
    }
}