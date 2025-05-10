using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface ITrackRepository : IBaseRepository<DTO.Track>, ITrackRepositoryCustom
{
}

public interface ITrackRepositoryCustom
{
    void CustomMethodTest();
    
    Task UpdateTrackWithRelationsAsync(DTO.Track track);
}