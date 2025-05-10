using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DTO.v1;

public class Playlist : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    [MaxLength(500)]
    public string Description { get; set; } = default!;

    [MaxLength(512)]
    public string? CoverUrl { get; set; }

    public bool IsPublic { get; set; }
    
    public string UserId { get; set; } = default!;
    
    public ICollection<TrackInPlaylist>? TracksInPlaylist { get; set; }
}