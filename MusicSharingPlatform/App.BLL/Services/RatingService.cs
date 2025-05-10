using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class RatingService : BaseService<App.BLL.DTO.Rating, App.DAL.DTO.Rating, App.DAL.Interfaces.IRatingRepository>, IRatingService
{
    public RatingService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.Rating, Rating, Guid> bllMapper) : base(serviceUOW, serviceUOW.RatingRepository, bllMapper)
    {
    }

    public async Task<bool> ExistsByTrackAndUserAsync(Guid trackId, string userId)
    {
        return await ServiceRepository.ExistsByTrackAndUserAsync(trackId, userId);
    }

    public async Task<IEnumerable<Rating>> GetAllByTrackIdAsync(Guid trackId)
    {
        return await ServiceRepository.GetAllByTrackIdAsync(trackId);
    }

    public async Task<double> GetAverageScoreAsync(Guid trackId)
    {
        var ratings = (await GetAllByTrackIdAsync(trackId)).ToList();
    
        if (ratings.Count == 0)
        {
            return 0.0;
        }

        return ratings.Average(r => r.Score);
    }
}