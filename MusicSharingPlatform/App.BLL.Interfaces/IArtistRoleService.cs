using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IArtistRoleService : IBaseService<App.BLL.DTO.ArtistRole>, IArtistRoleRepositoryCustom
{
    
}