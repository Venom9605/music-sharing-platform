using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Mood : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; } = default!;
    
    public ICollection<MoodsInTrack>? MoodsInTracks { get; set; }
    public ICollection<MoodsInPlaylist>? MoodsInPlaylists { get; set; }
}