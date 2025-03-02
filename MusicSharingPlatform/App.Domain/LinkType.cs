using System.ComponentModel.DataAnnotations;

namespace Domain;

public class LinkType : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; } = default!;
    
    public ICollection<UserLink>? UserLinks { get; set; }
    
    public ICollection<TrackLink>? TrackLinks { get; set; }
}