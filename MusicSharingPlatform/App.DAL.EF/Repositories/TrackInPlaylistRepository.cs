using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TrackInPlaylistRepository : BaseRepository<DTO.TrackInPlaylist, Domain.TrackInPlaylist>, ITrackInPlaylistRepository
{
    private readonly TrackInPlaylistUOWMapper _iuowMapper = new TrackInPlaylistUOWMapper();
    public TrackInPlaylistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TrackInPlaylistUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.TrackInPlaylist>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(t => t.Track)
            .Include(t => t.Playlist)
            .ToListAsync())
            .Select(e => _iuowMapper.Map(e)!);
    }
    
    public override async Task<DTO.TrackInPlaylist?> FindAsync(Guid id, string? userId)
    {
        var entity = await GetQuery(userId)
            .Include(r => r.Track)
            .Include(r => r.Playlist)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _iuowMapper.Map(entity);
    }
}