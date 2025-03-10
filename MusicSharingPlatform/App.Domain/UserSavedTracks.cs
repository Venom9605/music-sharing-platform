using Microsoft.AspNetCore.Identity;

namespace Domain;

public class UserSavedTracks : BaseEntity
{
    public Guid TrackId { get; set; }
    public Track? Track { get; set; }

    public string UserId { get; set; } = default!;
    public IdentityUser? User { get; set; } 

    
    private DateTime _savedAt;
    public DateTime SavedAt 
    {
        get => _savedAt;
        set => _savedAt = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
}