using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IArtistInTrackRepository : IBaseRepository<DTO.ArtistInTrack>, IArtistInTrackRepositoryCustom
{
}

public interface IArtistInTrackRepositoryCustom
{
    void CustomMethodTest();
}