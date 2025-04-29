using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class ArtistRoleService : BaseService<App.BLL.DTO.ArtistRole, App.DAL.DTO.ArtistRole, App.DAL.Interfaces.IArtistRoleRepository>, IArtistRoleService
{
    public ArtistRoleService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.ArtistRole, ArtistRole, Guid> bllMapper) : base(serviceUOW, serviceUOW.ArtistRoleRepository, bllMapper)
    {
    }

    public void CustomMethodTest()
    {
        ServiceRepository.CustomMethodTest();
    }
}