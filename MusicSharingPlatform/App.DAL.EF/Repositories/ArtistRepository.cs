using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Base.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ArtistRepository : BaseRepository<DTO.Artist, Domain.Artist, string>, IArtistRepository
{
    private readonly ArtistUOWMapper _iuowMapper = new ArtistUOWMapper();
    public ArtistRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ArtistUOWMapper())
    {
    }

    protected override IQueryable<Artist> GetQuery(string? userId)
    {
        var query = RepositoryDbSet.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(userId))
        {
            query = query.Where(e => e.Id == userId);
        }

        return query;
    }

    public override async Task<IEnumerable<DTO.Artist>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .ToListAsync())
            .Select(e => _iuowMapper.Map(e)!);
    }

    public override async Task<DTO.Artist?> FindAsync(string id, string? userId)
    {
        var query = GetQuery(userId);

        var res = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _iuowMapper.Map(res);
    }
    
    public async Task<DTO.Artist?> FindByNormalizedUserNameAsync(string normalizedUserName)
    {
        var res = await RepositoryDbSet
            .FirstOrDefaultAsync(a => a.NormalizedUserName == normalizedUserName);
        
        return _iuowMapper.Map(res);
    }
    
    public async Task<Domain.Artist?> FindTrackedDomainAsync(string id)
    {
        return await RepositoryDbSet
            .IgnoreQueryFilters()
            .AsTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }
    

    public void CustomMethodTest()
    {
        Console.WriteLine("Custom test Artist method called.");
    }
    
    public async Task<DTO.Artist?> GetMostPopularArtistAsync()
    {
        var artist = await RepositoryDbSet
            .Include(a => a.ArtistInTracks!) // join table
            .ThenInclude(ait => ait.Track)
            .Where(a => a.ArtistInTracks!.Any())
            .Select(a => new
            {
                Artist = a,
                PopularityScore = a.ArtistInTracks!
                    .Where(ait => ait.Track != null)
                    .Sum(ait => (ait.Track!.TimesPlayed + ait.Track.TimesSaved))
            })
            .OrderByDescending(x => x.PopularityScore)
            .Select(x => x.Artist)
            .FirstOrDefaultAsync();

        return artist == null ? null : _iuowMapper.Map(artist);
    }
    
    public async Task<List<DTO.Artist>> SearchArtistsAsync(string query)
    {
        return await RepositoryDbSet
            .Where(a => a.DisplayName.ToLower().Contains(query.ToLower()))
            .Select(a => _iuowMapper.Map(a)!)
            .ToListAsync();
    }
    
}