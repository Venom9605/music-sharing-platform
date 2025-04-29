using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IMoodRepository : IBaseRepository<DTO.Mood>, IMoodRepositoryCustom
{
    
}

public interface IMoodRepositoryCustom
{
    // Custom methods can be defined here
}