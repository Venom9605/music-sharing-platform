using System.ComponentModel.DataAnnotations;

namespace Domain;

public class TrackLink : BaseEntity
{
    public Guid TrackId { get; set; }
    public Track? Track { get; set; }
    
    public Guid LinkTypeId { get; set; }
    public LinkType? LinkType { get; set; }
    
    [MaxLength(500)]
    public string Url { get; set; } = default!;
}