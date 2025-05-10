using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class UserLinkBLLMapper : IBLLMapper<App.BLL.DTO.UserLink, App.DAL.DTO.UserLink>
{
    public UserLink? Map(DTO.UserLink? entity)
    {
        if (entity == null) return null;
        var res = new UserLink()
        {
            Id = entity.Id,
            
            UserId = entity.UserId,
            User = entity.User != null ? new Artist
            {
                Id = entity.User.Id,
                DisplayName = entity.User.DisplayName
            } : null,
            
            LinkTypeId = entity.LinkTypeId,
            LinkType = entity.LinkType != null ? new LinkType
            {
                Id = entity.LinkType.Id,
                Name = entity.LinkType.Name
            } : null,
            
            Url = entity.Url

        };
        return res;
    }

    public DTO.UserLink? Map(UserLink? entity)
    {
        if (entity == null) return null;
        var res = new DTO.UserLink()
        {
            Id = entity.Id,
            
            UserId = entity.UserId,
            User = entity.User != null ? new DTO.Artist
            {
                Id = entity.User.Id,
                DisplayName = entity.User.DisplayName
            } : null,
            
            LinkTypeId = entity.LinkTypeId,
            LinkType = entity.LinkType != null ? new DTO.LinkType
            {
                Id = entity.LinkType.Id,
                Name = entity.LinkType.Name
            } : null,
            
            Url = entity.Url

        };
        return res;
    }
}