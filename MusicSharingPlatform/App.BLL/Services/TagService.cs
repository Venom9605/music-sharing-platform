using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class TagService : BaseService<App.BLL.DTO.Tag, App.DAL.DTO.Tag, App.DAL.Interfaces.ITagRepository>, ITagService
{
    public TagService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.Tag, Tag, Guid> bllMapper) : base(serviceUOW, serviceUOW.TagRepository, bllMapper)
    {
    }
    
}