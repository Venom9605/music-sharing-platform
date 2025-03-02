using System.ComponentModel.DataAnnotations;

namespace Domain;

public class UserLink : BaseEntity
{
    public Guid ArtistId { get; set; }
    public Artist? Artist { get; set; }
    
    public Guid LinkTypeId { get; set; }
    public LinkType? LinkType { get; set; }
    
    [MaxLength(500)]
    public string Url { get; set; } = default!;
}