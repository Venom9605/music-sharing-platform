using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;

namespace App.DAL.EF.Repositories;

public class TrackRepository : BaseRepository<DTO.Track, Domain.Track>, ITrackRepository
{
    private readonly TrackMapper _mapper = new TrackMapper();
    public TrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TrackMapper())
    {
    }
    
}
