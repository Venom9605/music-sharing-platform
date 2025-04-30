using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface ITagsInTrackService : IBaseService<App.BLL.DTO.TagsInTrack>, ITagsInTrackRepositoryCustom
{
    
}