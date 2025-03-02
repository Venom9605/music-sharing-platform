namespace Domain;

public class TagsInTrack : BaseEntity
{
    public Guid TrackId { get; set; }
    public Track? Track { get; set; }
    
    public Guid TagId { get; set; }
    public Tag? Tag { get; set; }
}