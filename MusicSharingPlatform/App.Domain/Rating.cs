using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Rating : BaseEntity
{
    public Guid TrackId { get; set; }
    public Track? Track { get; set; }
    
    public Guid ArtistId { get; set; }
    public Artist? Artist { get; set; }
    
    public int Score { get; set; }
    
    [MaxLength(512)]
    public string? Comment { get; set; } = default!;
    
    public DateTime Date { get; set; }
    
}