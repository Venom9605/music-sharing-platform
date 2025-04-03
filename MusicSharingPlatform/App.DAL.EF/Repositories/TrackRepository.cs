using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;

namespace App.DAL.EF.Repositories;

public class TrackRepository : BaseRepository<Track>, ITrackRepository
{
    public TrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
}
