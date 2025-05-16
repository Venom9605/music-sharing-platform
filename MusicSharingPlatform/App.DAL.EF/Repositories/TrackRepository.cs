using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;
using Artist = App.DAL.DTO.Artist;
using Track = App.DAL.DTO.Track;

namespace App.DAL.EF.Repositories;

public class TrackRepository : BaseRepository<DTO.Track, Domain.Track>, ITrackRepository
{
    private readonly TrackUOWMapper _iuowMapper = new TrackUOWMapper();
    public TrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TrackUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.Track>> AllAsync(string? userId = null)
    {
        var query = RepositoryDbSet
            .Include(t => t.ArtistInTracks)!
            .ThenInclude(ait => ait.User)
            .Include(t => t.ArtistInTracks)!
            .ThenInclude(ait => ait.ArtistRole)
            .Include(t => t.Rating)!
            .ThenInclude(r => r.User)
            .Include(t => t.TrackLinks)!
            .ThenInclude(tl => tl.LinkType)
            .Include(t => t.TagsInTracks)!
            .ThenInclude(tit => tit.Tag)
            .Include(t => t.MoodsInTracks)!
            .ThenInclude(mit => mit.Mood)
            .Where(t => t.ArtistInTracks!.Any(ait => ait.UserId == userId));

        var result = await query.ToListAsync();
        return result.Select(e => _iuowMapper.Map(e)!);
    }

    public override async Task<Track?> FindAsync(Guid id, string? userId)
    {
        var query = GetQuery(userId)
            .Include(t => t.ArtistInTracks)!
            .ThenInclude(ait => ait.User)

            .Include(t => t.ArtistInTracks)!
            .ThenInclude(ait => ait.ArtistRole)

            .Include(t => t.Rating)!
            .ThenInclude(r => r.User)

            .Include(t => t.TrackLinks)!
            .ThenInclude(tl => tl.LinkType)

            .Include(t => t.TagsInTracks)!
            .ThenInclude(tit => tit.Tag)

            .Include(t => t.MoodsInTracks)!
            .ThenInclude(mit => mit.Mood);

        var res =  await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        
        return IuowMapper.Map(res);
    }
    
    

    public void CustomMethodTest()
    {
        Console.WriteLine("Custom test Track method called.");
    }

    public async Task UpdateTrackWithRelationsAsync(Track track)
    {
        var existing = await RepositoryDbSet
            .Include(t => t.ArtistInTracks)
            .Include(t => t.TagsInTracks)
            .Include(t => t.MoodsInTracks)
            .Include(t => t.TrackLinks)
            .AsTracking()
            .FirstOrDefaultAsync(t => t.Id == track.Id);
        
        if (existing == null) return;
        
        existing.Title = track.Title;
        existing.CoverPath = track.CoverPath;
        
        RepositoryDbContext.RemoveRange(existing.ArtistInTracks!);
        RepositoryDbContext.RemoveRange(existing.TagsInTracks!);
        RepositoryDbContext.RemoveRange(existing.MoodsInTracks!);
        RepositoryDbContext.RemoveRange(existing.TrackLinks!);


        if (track.ArtistInTracks != null)
        {
            var newArtistLinks = track.ArtistInTracks.Select(a => new Domain.ArtistInTrack
            {
                Id = Guid.NewGuid(),
                TrackId = a.TrackId,
                UserId = a.UserId,
                ArtistRoleId = a.ArtistRoleId
            });
            await RepositoryDbContext.AddRangeAsync(newArtistLinks);
        }

        if (track.TagsInTracks != null)
        {
            var newTags = track.TagsInTracks.Select(t => new Domain.TagsInTrack
            {
                Id = Guid.NewGuid(),
                TrackId = t.TrackId,
                TagId = t.TagId
            });
            await RepositoryDbContext.AddRangeAsync(newTags);
        }

        if (track.MoodsInTracks != null)
        {
            var newMoods = track.MoodsInTracks.Select(m => new Domain.MoodsInTrack
            {
                Id = Guid.NewGuid(),
                TrackId = m.TrackId,
                MoodId = m.MoodId
            });
            await RepositoryDbContext.AddRangeAsync(newMoods);
        }

        if (track.TrackLinks != null)
        {
            var newLinks = track.TrackLinks.Select(l => new Domain.TrackLink
            {
                Id = Guid.NewGuid(),
                TrackId = l.TrackId,
                LinkTypeId = l.LinkTypeId,
                Url = l.Url
            });
            await RepositoryDbContext.AddRangeAsync(newLinks);
        }
    }

    public async Task<Track?> GetRandomTrackFilteredAsync(IEnumerable<Guid> tagIds, IEnumerable<Guid> moodIds)
    {
        var query = RepositoryDbSet
            .Include(t => t.ArtistInTracks)!.ThenInclude(ait => ait.User)
            .Include(t => t.ArtistInTracks)!.ThenInclude(ait => ait.ArtistRole)
            .Include(t => t.Rating)!.ThenInclude(r => r.User)
            .Include(t => t.TrackLinks)!.ThenInclude(tl => tl.LinkType)
            .Include(t => t.TagsInTracks)!.ThenInclude(tit => tit.Tag)
            .Include(t => t.MoodsInTracks)!.ThenInclude(mit => mit.Mood)
            .AsQueryable();

        if (tagIds.Any())
        {
            query = query.Where(t => tagIds.All(tagId => 
                t.TagsInTracks!.Any(tit => tit.TagId == tagId)));
        }

        if (moodIds.Any())
        {
            query = query.Where(t => moodIds.All(moodId =>
                t.MoodsInTracks!.Any(mit => mit.MoodId == moodId)));
        }

        var filtered = await query.OrderBy(t => Guid.NewGuid()).FirstOrDefaultAsync();
        return _iuowMapper.Map(filtered);
    }

    

    public async Task<Domain.Track?> FindTrackedDomainAsync(Guid id)
    {
        return await RepositoryDbSet
            .AsTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }
    
    public async Task<List<Track>> SearchTracksAsync(string query)
    {
        return await RepositoryDbSet
            .Where(t => t.Title.ToLower().Contains(query.ToLower()))
            .Include(t => t.ArtistInTracks)!.ThenInclude(a => a.User)
            .Include(t => t.TrackLinks)!.ThenInclude(tl => tl.LinkType)
            .Include(t => t.TagsInTracks)!.ThenInclude(tt => tt.Tag)
            .Include(t => t.MoodsInTracks)!.ThenInclude(mt => mt.Mood)
            .Select(t => _iuowMapper.Map(t)!)
            .ToListAsync();
    }
}
