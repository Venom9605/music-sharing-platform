using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IArtistRepository : IRepository<DTO.Artist, string>, IArtistRepositoryCustom
{
    
}

public interface IArtistRepositoryCustom
{
    void CustomMethodTest();
    
    
    Task<Artist?> FindByNormalizedUserNameAsync(string normalizedUserName);

}