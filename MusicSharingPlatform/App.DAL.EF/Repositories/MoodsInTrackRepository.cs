using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MoodsInTrackRepository : BaseRepository<MoodsInTrack>, IMoodsInTrackRepository
{
    public MoodsInTrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<MoodsInTrack>> AllAsync(string? userId = null)
    {
        return await GetQuery(userId)
            .Include(m => m.Mood)
            .Include(m => m.Track)
            .ToListAsync();
    }
}