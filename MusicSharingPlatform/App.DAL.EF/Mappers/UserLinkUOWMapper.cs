using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class UserLinkUOWMapper : IUOWMapper<DTO.UserLink, Domain.UserLink>
{
    public UserLink? Map(Domain.UserLink? entity)
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

    public Domain.UserLink? Map(UserLink? entity)
    {
        if (entity == null) return null;
        var res = new Domain.UserLink()
        {
            Id = entity.Id,
            UserId = entity.UserId,
            User = null,
            LinkTypeId = entity.LinkTypeId,
            LinkType = null,
            Url = entity.Url

        };
        return res;
    }
}