using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IRatingRepository : IBaseRepository<DTO.Rating>, IRatingRepositoryCustom
{
    
}

public interface IRatingRepositoryCustom
{
    Task<bool> ExistsByTrackAndUserAsync(Guid trackId, string userId);
    
    Task<IEnumerable<App.DAL.DTO.Rating>> GetAllByTrackIdAsync(Guid trackId);
}