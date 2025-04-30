using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IUserLinkService : IBaseService<App.BLL.DTO.UserLink>, IUserLinkRepositoryCustom
{
    
}