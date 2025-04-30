using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IMoodService : IBaseService<App.BLL.DTO.Mood>, IMoodRepositoryCustom
{
    
}