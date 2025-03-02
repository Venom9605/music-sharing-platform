using System.ComponentModel.DataAnnotations;

namespace Domain;

public class ArtistRole : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; } = default!;
    
    public ICollection<ArtistInTrack>? ArtistInTracks { get; set; }
}