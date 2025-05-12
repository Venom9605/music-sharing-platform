using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class ArtistService : BaseService<App.BLL.DTO.Artist, App.DAL.DTO.Artist, App.DAL.Interfaces.IArtistRepository, string>, IArtistService
{
    public ArtistService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.Artist, Artist, string> bllMapper) : base(serviceUOW, serviceUOW.ArtistRepository, bllMapper)
    {
    }

    public void CustomMethodTest()
    {
        ServiceRepository.CustomMethodTest();
    }

    public Task<Artist?> FindByNormalizedUserNameAsync(string normalizedUserName)
    {
        var res = ServiceRepository.FindByNormalizedUserNameAsync(normalizedUserName);
        
        return res;
    }
}