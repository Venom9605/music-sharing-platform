using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TrackLinkRepository : BaseRepository<DTO.TrackLink, Domain.TrackLink>, ITrackLinkRepository
{
    private readonly TrackLinkUOWMapper _iuowMapper = new TrackLinkUOWMapper();
    public TrackLinkRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TrackLinkUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<DTO.TrackLink>> AllAsync(string? userId = null)
    {
        return (await GetQuery(userId)
            .Include(t => t.Track)
            .Include(t => t.LinkType)
            .ToListAsync())
            .Select(e => _iuowMapper.Map(e)!);
    }
}