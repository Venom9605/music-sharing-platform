using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class Playlist : BaseEntity
{
    public string UserId { get; set; } = default!;
    public IdentityUser? User { get; set; } 

    [MaxLength(100)]
    public string Name { get; set; } = default!;
    
    [MaxLength(500)]
    public string Description { get; set; } = default!;
    
    [MaxLength(500)]
    public string? CoverUrl { get; set; } = default!;
    
    public bool IsPublic { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public ICollection<TagsInPlaylist>? TagsInPlaylists { get; set; }
    
    public ICollection<MoodsInPlaylist>? MoodsInPlaylists { get; set; }
    
    public ICollection<TrackInPlaylist>? TrackInPlaylists { get; set; }
}