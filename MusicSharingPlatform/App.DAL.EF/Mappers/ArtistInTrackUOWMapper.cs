using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class ArtistInTrackUOWMapper : IUOWMapper<DTO.ArtistInTrack, Domain.ArtistInTrack>
{
    public ArtistInTrack? Map(Domain.ArtistInTrack? entity)
    {
        if (entity == null) return null;
        
        Console.WriteLine("Mapping from Domain to DAL DTO: " + entity.Id);
        
        var res = new ArtistInTrack()
        {
            Id = entity.Id,
            
            TrackId = entity.TrackId,
            Track = entity.Track != null ? new Track
            {
                Id = entity.Track.Id,
                Title = entity.Track.Title
            } : null,
            
            UserId = entity.UserId,
            User = entity.User != null ? new Artist
            {
                Id = entity.User.Id,
                DisplayName = entity.User.DisplayName
            } : null,
            
            ArtistRoleId = entity.ArtistRoleId,
            ArtistRole = entity.ArtistRole != null ? new ArtistRole
            {
                Id = entity.ArtistRole.Id,
                Name = entity.ArtistRole.Name
            } : null
            
        };
        
        
        Console.WriteLine(res.Track!.Title);
        Console.WriteLine(res.User!.DisplayName);
        Console.WriteLine(res.ArtistRole!.Name);
        
        return res;

        
        
    }

    public Domain.ArtistInTrack? Map(ArtistInTrack? entity)
    {
        if (entity == null) return null;
        
        Console.WriteLine("Mapping from DAL DTO to Domain: " + entity.Id);
        
        var res = new Domain.ArtistInTrack()
        {
            Id = entity.Id,
            
            TrackId = entity.TrackId,
            Track = null,
            
            UserId = entity.UserId,
            User = null,
            
            ArtistRoleId = entity.ArtistRoleId,
            ArtistRole = null
            
        };
        return res;
    }
}