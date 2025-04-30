using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class LinkTypeService : BaseService<App.BLL.DTO.LinkType, App.DAL.DTO.LinkType, App.DAL.Interfaces.ILinkTypeRepository>, ILinkTypeService
{
    public LinkTypeService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.LinkType, LinkType, Guid> bllMapper) : base(serviceUOW, serviceUOW.LinkTypeRepository, bllMapper)
    {
    }
    
}