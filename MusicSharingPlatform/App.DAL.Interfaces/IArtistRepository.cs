using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IArtistRepository : IRepository<DTO.Artist, string>, IArtistRepositoryCustom
{
    Task<Domain.Artist?> FindTrackedDomainAsync(string id);
    
    Task<DTO.Artist?> GetMostPopularArtistAsync();
    
    Task<List<Artist>> SearchArtistsAsync(string query);
}

public interface IArtistRepositoryCustom
{
    void CustomMethodTest();
    
    Task<Artist?> FindByNormalizedUserNameAsync(string normalizedUserName);
    
}