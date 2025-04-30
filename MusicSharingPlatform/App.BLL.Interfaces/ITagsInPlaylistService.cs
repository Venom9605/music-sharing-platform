using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface ITagsInPlaylistService : IBaseService<App.BLL.DTO.TagsInPlaylist>, ITagsInPlaylistRepositoryCustom
{
    
}