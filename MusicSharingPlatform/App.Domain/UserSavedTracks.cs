namespace Domain;

public class UserSavedTracks : BaseEntity
{
    public Guid TrackId { get; set; }
    public Track? Track { get; set; }

    public Guid ArtistId { get; set; }
    public Artist? Artist { get; set; }

    public DateTime SavedAt { get; set; }
}