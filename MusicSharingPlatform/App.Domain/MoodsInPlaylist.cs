namespace Domain;

public class MoodsInPlaylist : BaseEntity
{
    public Guid MoodId { get; set; }
    public Mood? Mood { get; set; }
    
    public Guid PlaylistId { get; set; }
    public Playlist? Playlist { get; set; }
}