using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface ITrackRepository : IBaseRepository<DTO.Track>, ITrackRepositoryCustom
{
    Task<Domain.Track?> FindTrackedDomainAsync(Guid id);
    
}

public interface ITrackRepositoryCustom
{
    void CustomMethodTest();
    
    Task UpdateTrackWithRelationsAsync(DTO.Track track);
    
    Task<DTO.Track?> GetRandomTrackAsync();
    
}