using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class TrackLinkBLLMapper : IBLLMapper<App.BLL.DTO.TrackLink, App.DAL.DTO.TrackLink>
{
    public TrackLink? Map(DTO.TrackLink? entity)
    {
        if (entity == null) return null;
        var res = new TrackLink()
        {
            Id = entity.Id,
            
            TrackId = entity.TrackId,
            Track = entity.Track != null ? new Track
            {
                Id = entity.Track.Id,
                Title = entity.Track.Title
            } : null,
            
            LinkTypeId = entity.LinkTypeId,
            LinkType = entity.LinkType != null ? new LinkType
            {
                Id = entity.LinkType.Id,
                Name = entity.LinkType.Name
            } : null,
            
            Url = entity.Url,
            

        };
        return res;
    }

    public DTO.TrackLink? Map(TrackLink? entity)
    {
        if (entity == null) return null;
        var res = new DTO.TrackLink()
        {
            Id = entity.Id,
            
            TrackId = entity.TrackId,
            Track = entity.Track != null ? new DTO.Track
            {
                Id = entity.Track.Id,
                Title = entity.Track.Title
            } : null,
            
            LinkTypeId = entity.LinkTypeId,
            LinkType = entity.LinkType != null ? new DTO.LinkType
            {
                Id = entity.LinkType.Id,
                Name = entity.LinkType.Name
            } : null,
            
            Url = entity.Url,
            

        };
        return res;
    }
}