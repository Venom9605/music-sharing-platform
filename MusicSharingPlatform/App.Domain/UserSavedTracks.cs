using Microsoft.AspNetCore.Identity;

namespace Domain;

public class UserSavedTracks : BaseEntity
{
    public Guid TrackId { get; set; }
    public Track? Track { get; set; }

    public string UserId { get; set; } = default!;
    public IdentityUser? User { get; set; } 

    public DateTime SavedAt { get; set; }
}