using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class Mood : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    [MaxLength(100)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Mood))]
    public string Name { get; set; } = default!;
    
    public ICollection<MoodsInTrack>? MoodsInTracks { get; set; }
    public ICollection<MoodsInPlaylist>? MoodsInPlaylists { get; set; }
}