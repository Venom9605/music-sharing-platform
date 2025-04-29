using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ArtistInTrackRepository : BaseRepository<DTO.ArtistInTrack, Domain.ArtistInTrack>, IArtistInTrackRepository
{
    private readonly ArtistInTrackUOWMapper _iuowMapper = new ArtistInTrackUOWMapper();
    
    public ArtistInTrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ArtistInTrackUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.ArtistInTrack>> AllAsync(string? userId = null)
    {
        var query = GetQuery(userId)
            .Include(a => a.Track)
            .Include(a => a.User)
            .Include(a => a.ArtistRole);

        var result = await query.ToListAsync();
        foreach (var entity in result)
        {
            Console.WriteLine($"Fetched ArtistInTrack: {entity.Id}, Track: {entity.Track?.Title}, User: {entity.User?.DisplayName}, Role: {entity.ArtistRole?.Name}");
        }

        return result.Select(e => _iuowMapper.Map(e)!);
    }

    public override async Task<DTO.ArtistInTrack?> FindAsync(Guid id, string? userId)
    {
        var res = await GetQuery(userId)
            .Include(a => a.Track)
            .Include(a => a.User)
            .Include(a => a.ArtistRole)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _iuowMapper.Map(res);
    }

    public void CustomMethodTest()
    {
        Console.WriteLine("Custom test ArtistInTrack method called.");
    }
}