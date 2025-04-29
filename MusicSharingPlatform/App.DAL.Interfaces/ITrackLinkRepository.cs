using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface ITrackLinkRepository : IBaseRepository<DTO.TrackLink>, ITrackLinkRepositoryCustom
{
    
}

public interface ITrackLinkRepositoryCustom
{
}