using Microsoft.AspNetCore.Identity;

namespace Domain;

public class ArtistInTrack : BaseEntity
{
    public Guid TrackId { get; set; }
    public Track? Track { get; set; }

    public string UserId { get; set; } = default!;
    public IdentityUser? User { get; set; } 
    
    public Guid ArtistRoleId { get; set; }
    public ArtistRole? ArtistRole { get; set; }
}