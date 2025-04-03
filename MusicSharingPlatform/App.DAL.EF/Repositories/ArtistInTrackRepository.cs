using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ArtistInTrackRepository : BaseRepository<ArtistInTrack>, IArtistInTrackRepository
{
    public ArtistInTrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<ArtistInTrack>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .Include(a => a.Track)
            .Include(a => a.User)
            .Include(a => a.ArtistRole)
            .ToListAsync();
    }

    public override async Task<ArtistInTrack?> FindAsync(Guid id, string? userId)
    {
        return await GetQuery(userId)
            .Include(a => a.Track)
            .Include(a => a.User)
            .Include(a => a.ArtistRole)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));
    }
}