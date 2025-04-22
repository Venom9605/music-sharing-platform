using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TrackInPlaylistRepository : BaseRepository<DTO.TrackInPlaylist, Domain.TrackInPlaylist>, ITrackInPlaylistRepository
{
    private readonly TrackInPlaylistMapper _mapper = new TrackInPlaylistMapper();
    public TrackInPlaylistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TrackInPlaylistMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.TrackInPlaylist>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(t => t.Track)
            .Include(t => t.Playlist)
            .ToListAsync())
            .Select(e => _mapper.Map(e)!);
    }
}