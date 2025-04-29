using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using Base.Dal.EF;
using Domain;

namespace App.DAL.EF.Repositories;

public class TrackRepository : BaseRepository<DTO.Track, Domain.Track>, ITrackRepository
{
    private readonly TrackUOWMapper _iuowMapper = new TrackUOWMapper();
    public TrackRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TrackUOWMapper())
    {
    }

    public void CustomMethodTest()
    {
        Console.WriteLine("Custom test Track method called.");
    }
}
