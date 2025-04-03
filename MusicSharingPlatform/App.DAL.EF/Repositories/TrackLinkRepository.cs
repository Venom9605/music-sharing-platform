using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TrackLinkRepository : BaseRepository<TrackLink>, ITrackLinkRepository
{
    public TrackLinkRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<TrackLink>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .Include(t => t.Track)
            .Include(t => t.LinkType)
            .ToListAsync();
    }
}