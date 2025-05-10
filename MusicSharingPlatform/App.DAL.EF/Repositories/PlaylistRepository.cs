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
        var query = GetQuery(userId)
            .Include(p => p.TrackInPlaylists)!
            .ThenInclude(tip => tip.Track)!
            .ThenInclude(t => t!.ArtistInTracks)!
            .ThenInclude(ait => ait.User)

            .Include(p => p.TrackInPlaylists)!
            .ThenInclude(tip => tip.Track)!
            .ThenInclude(t => t!.ArtistInTracks)!
            .ThenInclude(ait => ait.ArtistRole)

            .Include(p => p.TrackInPlaylists)!
            .ThenInclude(tip => tip.Track)!
            .ThenInclude(t => t!.Rating)!
            .ThenInclude(r => r.User)

            .Include(p => p.TrackInPlaylists)!
            .ThenInclude(tip => tip.Track)!
            .ThenInclude(t => t!.TrackLinks)!
            .ThenInclude(l => l.LinkType)

            .Include(p => p.TrackInPlaylists)!
            .ThenInclude(tip => tip.Track)!
            .ThenInclude(t => t!.TagsInTracks)!
            .ThenInclude(tag => tag.Tag)

            .Include(p => p.TrackInPlaylists)!
            .ThenInclude(tip => tip.Track)!
            .ThenInclude(t => t!.MoodsInTracks)!
            .ThenInclude(mood => mood.Mood);

        var result = await query.ToListAsync();

        return result.Select(e => _iuowMapper.Map(e)!);
    }
    
    public override async Task<DTO.Playlist?> FindAsync(Guid id, string? userId)
    {
        var query = GetQuery(userId)
            .Include(p => p.TrackInPlaylists)!
            .ThenInclude(tip => tip.Track)!
            .ThenInclude(t => t!.ArtistInTracks)!
            .ThenInclude(ait => ait.User)

            .Include(p => p.TrackInPlaylists)!
            .ThenInclude(tip => tip.Track)!
            .ThenInclude(t => t!.ArtistInTracks)!
            .ThenInclude(ait => ait.ArtistRole)

            .Include(p => p.TrackInPlaylists)!
            .ThenInclude(tip => tip.Track)!
            .ThenInclude(t => t!.Rating)!
            .ThenInclude(r => r.User)

            .Include(p => p.TrackInPlaylists)!
            .ThenInclude(tip => tip.Track)!
            .ThenInclude(t => t!.TrackLinks)!
            .ThenInclude(l => l.LinkType)

            .Include(p => p.TrackInPlaylists)!
            .ThenInclude(tip => tip.Track)!
            .ThenInclude(t => t!.TagsInTracks)!
            .ThenInclude(tag => tag.Tag)

            .Include(p => p.TrackInPlaylists)!
            .ThenInclude(tip => tip.Track)!
            .ThenInclude(t => t!.MoodsInTracks)!
            .ThenInclude(mood => mood.Mood);

        var result = await query.FirstOrDefaultAsync(e => e.Id == id);
        return _iuowMapper.Map(result);
    }
}