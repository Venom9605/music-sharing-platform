using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IArtistRepository : IRepository<DTO.Artist, string>, IArtistRepositoryCustom
{
    
}

public interface IArtistRepositoryCustom
{
    void CustomMethodTest();

}