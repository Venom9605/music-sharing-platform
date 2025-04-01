using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain;

public class ArtistRole : BaseEntity
{
    [MaxLength(100)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.ArtistRole))]
    public string Name { get; set; } = default!;
    
    public ICollection<ArtistInTrack>? ArtistInTracks { get; set; }
}