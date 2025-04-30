using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class TagsInTrackService : BaseService<App.BLL.DTO.TagsInTrack, App.DAL.DTO.TagsInTrack, App.DAL.Interfaces.ITagsInTrackRepository>, ITagsInTrackService
{
    public TagsInTrackService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.TagsInTrack, TagsInTrack, Guid> bllMapper) : base(serviceUOW, serviceUOW.TagsInTrackRepository, bllMapper)
    {
    }
    
}