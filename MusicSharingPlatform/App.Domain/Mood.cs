using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain;

public class Mood : BaseEntity
{
    [MaxLength(100)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Mood))]
    public string Name { get; set; } = default!;
    
    public ICollection<MoodsInTrack>? MoodsInTracks { get; set; }
    public ICollection<MoodsInPlaylist>? MoodsInPlaylists { get; set; }
}