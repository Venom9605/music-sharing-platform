using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Tag : BaseEntity
{
    [MaxLength(100)]   
    public string Name { get; set; } = default!;
    
    public ICollection<TagsInTrack>? TagsInTracks { get; set; }
    
    public ICollection<TagsInPlaylist>? TagsInPlaylists { get; set; }
}