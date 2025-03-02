namespace Domain;

public class TrackInPlaylist : BaseEntity
{
    public Guid TrackId { get; set; }
    public Track? Track { get; set; }
    
    public Guid PlaylistId { get; set; }
    public Playlist? Playlist { get; set; }
}