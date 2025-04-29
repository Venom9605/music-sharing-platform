using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IArtistService : IBaseService<App.BLL.DTO.Artist, string>, IArtistRepositoryCustom
{
    
}