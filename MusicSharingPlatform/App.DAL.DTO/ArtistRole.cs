using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DAL.DTO;

public class ArtistRole : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }

    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.ArtistRole))]
    public string Name { get; set; } = default!;
    
    public ICollection<ArtistInTrack>? ArtistInTracks { get; set; }
}