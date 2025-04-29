using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface ITagRepository : IBaseRepository<DTO.Tag>, ITagRepositoryCustom
{
    
}

public interface ITagRepositoryCustom
{
}