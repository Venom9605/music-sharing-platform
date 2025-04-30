using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class Playlist : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.Playlist))]
    public string UserId { get; set; } = default!;
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.Playlist))]
    public Artist? User { get; set; } 

    
    [MaxLength(100)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Playlist))]
    public string Name { get; set; } = default!;
    
    
    [MaxLength(500)]
    [Display(Name = nameof(Description), Prompt = nameof(Description), ResourceType = typeof(App.Resources.Domain.Playlist))]
    public string Description { get; set; } = default!;
    
    
    [MaxLength(500)]
    [Display(Name = nameof(CoverUrl), Prompt = nameof(CoverUrl), ResourceType = typeof(App.Resources.Domain.Playlist))]
    public string? CoverUrl { get; set; } = default!;
    
    
    [Display(Name = nameof(IsPublic), Prompt = nameof(IsPublic), ResourceType = typeof(App.Resources.Domain.Playlist))]
    public bool IsPublic { get; set; }
    
    public ICollection<TagsInPlaylist>? TagsInPlaylists { get; set; }
    
    public ICollection<MoodsInPlaylist>? MoodsInPlaylists { get; set; }
    
    public ICollection<TrackInPlaylist>? TrackInPlaylists { get; set; }
}