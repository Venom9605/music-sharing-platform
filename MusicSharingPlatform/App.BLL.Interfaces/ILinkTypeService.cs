using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface ILinkTypeService : IBaseService<App.BLL.DTO.LinkType>, ILinkTypeRepositoryCustom
{
    
}