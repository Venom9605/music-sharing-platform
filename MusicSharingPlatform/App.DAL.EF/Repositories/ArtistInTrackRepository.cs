using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ArtistInTrackRepository : BaseRepository<DTO.ArtistInTrack, Domain.ArtistInTrack>, IArtistInTrackRepository
{
    private readonly ArtistInTrackMapper _mapper = new ArtistInTrackMapper();
    
    public ArtistInTrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ArtistInTrackMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.ArtistInTrack>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(a => a.Track)
            .Include(a => a.User)
            .Include(a => a.ArtistRole)
            .ToListAsync())
            .Select(e => _mapper.Map(e)!);
    }

    public override async Task<DTO.ArtistInTrack?> FindAsync(Guid id, string? userId)
    {
        var res = await GetQuery(userId)
            .Include(a => a.Track)
            .Include(a => a.User)
            .Include(a => a.ArtistRole)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));

        return _mapper.Map(res);
    }
}