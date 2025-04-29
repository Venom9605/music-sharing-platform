using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IMoodsInTrackRepository : IBaseRepository<DTO.MoodsInTrack>, IMoodsInTrackRepositoryCustom
{
    
}

public interface IMoodsInTrackRepositoryCustom
{
    // Custom methods can be defined here
}