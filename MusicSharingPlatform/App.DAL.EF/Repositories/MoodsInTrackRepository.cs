using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MoodsInTrackRepository : BaseRepository<DTO.MoodsInTrack, Domain.MoodsInTrack>, IMoodsInTrackRepository
{
    private readonly MoodsInTrackMapper _mapper = new MoodsInTrackMapper();
    public MoodsInTrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new MoodsInTrackMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.MoodsInTrack>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(m => m.Mood)
            .Include(m => m.Track)
            .ToListAsync())
            .Select(e => _mapper.Map(e)!);
    }
}