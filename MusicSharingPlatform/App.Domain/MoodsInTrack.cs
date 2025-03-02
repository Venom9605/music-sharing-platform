namespace Domain;

public class MoodsInTrack : BaseEntity
{
    public Guid MoodId { get; set; }
    public Mood? Mood { get; set; }
    
    public Guid TrackId { get; set; }
    public Track? Track { get; set; }
}