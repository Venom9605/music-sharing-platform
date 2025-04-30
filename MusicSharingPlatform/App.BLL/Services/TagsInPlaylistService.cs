using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class TagsInPlaylistService : BaseService<App.BLL.DTO.TagsInPlaylist, App.DAL.DTO.TagsInPlaylist, App.DAL.Interfaces.ITagsInPlaylistRepository>, ITagsInPlaylistService
{
    public TagsInPlaylistService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.TagsInPlaylist, TagsInPlaylist, Guid> bllMapper) : base(serviceUOW, serviceUOW.TagsInPlaylistRepository, bllMapper)
    {
    }
    
}