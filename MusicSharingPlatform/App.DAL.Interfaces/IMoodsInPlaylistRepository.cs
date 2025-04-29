using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IMoodsInPlaylistRepository : IBaseRepository<DTO.MoodsInPlaylist>, IMoodsInPlaylistRepositoryCustom
{
    
}

public interface IMoodsInPlaylistRepositoryCustom
{
    // Custom methods can be defined here
}