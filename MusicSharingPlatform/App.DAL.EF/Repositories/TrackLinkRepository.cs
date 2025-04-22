using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TrackLinkRepository : BaseRepository<DTO.TrackLink, Domain.TrackLink>, ITrackLinkRepository
{
    private readonly TrackLinkMapper _mapper = new TrackLinkMapper();
    public TrackLinkRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TrackLinkMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.TrackLink>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(t => t.Track)
            .Include(t => t.LinkType)
            .ToListAsync())
            .Select(e => _mapper.Map(e)!);
    }
}