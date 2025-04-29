using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IRatingRepository : IBaseRepository<DTO.Rating>, IRatingRepositoryCustom
{
    
}

public interface IRatingRepositoryCustom
{
}