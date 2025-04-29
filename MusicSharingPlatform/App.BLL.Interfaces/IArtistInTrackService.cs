using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IArtistInTrackService : IBaseService<App.BLL.DTO.ArtistInTrack>, IArtistInTrackRepositoryCustom
{
    
}