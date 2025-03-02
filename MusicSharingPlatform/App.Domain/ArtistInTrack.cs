using Microsoft.AspNetCore.Identity;

namespace Domain;

public class ArtistInTrack : BaseEntity
{
    public Guid TrackId { get; set; }
    public Track? Track { get; set; }

    public Guid ArtistId { get; set; }
    public Artist? Artist { get; set; }
    
    public Guid ArtistRoleId { get; set; }
    public ArtistRole? ArtistRole { get; set; }
}