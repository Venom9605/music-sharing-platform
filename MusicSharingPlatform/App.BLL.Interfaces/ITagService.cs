using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface ITagService : IBaseService<App.BLL.DTO.Tag>, ITagRepositoryCustom
{
    
}