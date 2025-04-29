
using App.DAL.DTO;
using Base.BLL.Interfaces;

namespace App.BLL.Mappers;

public class ArtistInTrackBLLMapper : IBLLMapper<App.BLL.DTO.ArtistInTrack, App.DAL.DTO.ArtistInTrack>
{
    public ArtistInTrack? Map(DTO.ArtistInTrack? entity)
    {
        if (entity == null) return null;
        
        Console.WriteLine("Mapping from BLL DTO to DAL DTO: " + entity.Id);
        
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
        return res;
    }

    
    public DTO.ArtistInTrack? Map(ArtistInTrack? entity)
    {
        if (entity == null) return null;
        
        Console.WriteLine("Mapping from DAL DTO to BLL DTO: " + entity.Id);
        
        var res = new DTO.ArtistInTrack()
        {
            Id = entity.Id,
        
            TrackId = entity.TrackId,
            Track = entity.Track != null ? new DTO.Track
            {
                Id = entity.Track.Id,
                Title = entity.Track.Title
            } : null,
        
            UserId = entity.UserId,
            User = entity.User != null ? new DTO.Artist
            {
                Id = entity.User.Id,
                DisplayName = entity.User.DisplayName
            } : null,
        
            ArtistRoleId = entity.ArtistRoleId,
            ArtistRole = entity.ArtistRole != null ? new DTO.ArtistRole
            {
                Id = entity.ArtistRole.Id,
                Name = entity.ArtistRole.Name
            } : null
        };
    
        Console.WriteLine(res.Track?.Title);
        Console.WriteLine(res.User?.DisplayName);
        Console.WriteLine(res.ArtistRole?.Name);
    
        return res;
    }
}