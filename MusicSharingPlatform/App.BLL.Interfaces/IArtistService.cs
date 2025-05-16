using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IArtistService : IBaseService<App.BLL.DTO.Artist, string>, IArtistRepositoryCustom
{
    
    Task<Domain.Artist?> FindTrackedAsync(string id);
    
    Task<App.BLL.DTO.Artist?> GetMostPopularArtistAsync();
    
    Task<List<App.BLL.DTO.Artist>> SearchArtistsAsync(string query);
}