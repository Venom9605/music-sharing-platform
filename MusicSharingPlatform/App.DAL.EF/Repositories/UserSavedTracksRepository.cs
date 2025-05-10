using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;
using Track = App.DAL.DTO.Track;

namespace App.DAL.EF.Repositories;

public class UserSavedTracksRepository : BaseRepository<DTO.UserSavedTracks, Domain.UserSavedTracks>, IUserSavedTracksRepository
{
    private readonly UserSavedTracksUOWMapper _iuowMapper = new UserSavedTracksUOWMapper();
    private readonly TrackUOWMapper _trackMapper = new TrackUOWMapper();
    public UserSavedTracksRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new UserSavedTracksUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.UserSavedTracks>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(u => u.User)
            .Include(u => u.Track)
            .ToListAsync())
            .Select(e => _iuowMapper.Map(e)!);
    }
    
    public override async Task<DTO.UserSavedTracks?> FindAsync(Guid id, string? userId)
    {
        var entity = await GetQuery(userId)
            .Include(r => r.User)
            .Include(r => r.Track)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _iuowMapper.Map(entity);
    }

    public async Task<IEnumerable<Track>> GetFullSavedTracksAsync(string userId)
    {
        var tracks = await RepositoryDbContext.Set<Domain.UserSavedTracks>()
            .Where(x => x.UserId == userId)
            .Include(x => x.Track) // Eager load Track first
            .ThenInclude(t => t!.ArtistInTracks)!.ThenInclude(a => a.User)
            .Include(x => x.Track)!.ThenInclude(t => t!.ArtistInTracks)!.ThenInclude(a => a.ArtistRole)
            .Include(x => x.Track)!.ThenInclude(t => t!.TagsInTracks)!.ThenInclude(t => t.Tag)
            .Include(x => x.Track)!.ThenInclude(t => t!.MoodsInTracks)!.ThenInclude(m => m.Mood)
            .Include(x => x.Track)!.ThenInclude(t => t!.TrackLinks)!.ThenInclude(l => l.LinkType)
            .Include(x => x.Track)!.ThenInclude(t => t!.Rating)!.ThenInclude(r => r.User)
            .Select(x => x.Track!)
            .ToListAsync();

        return tracks.Select(t => _trackMapper.Map(t)!);
    }

    public async Task<bool> ExistsByTrackAndUserAsync(Guid trackId, string userId)
    {
        return await RepositoryDbContext
            .Set<Domain.UserSavedTracks>()
            .AnyAsync(x => x.TrackId == trackId && x.UserId == userId);
    }

    public async Task RemoveByTrackIdAsync(Guid trackId, string userId)
    {
        var entity = await RepositoryDbContext
            .Set<Domain.UserSavedTracks>()
            .FirstOrDefaultAsync(x => x.TrackId == trackId && x.UserId == userId);

        if (entity != null)
        {
            RepositoryDbContext.Remove(entity);
        }
    }
}