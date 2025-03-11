using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class Playlist : BaseEntity
{
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.Playlist))]
    public string UserId { get; set; } = default!;
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.Playlist))]
    public IdentityUser? User { get; set; } 

    
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
    
    private DateTime _createdAt;
    
    
    [Display(Name = nameof(CreatedAt), Prompt = nameof(CreatedAt), ResourceType = typeof(App.Resources.Domain.Playlist))]
    public DateTime CreatedAt
    {
        get => _createdAt;
        set => _createdAt = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
    
    public ICollection<TagsInPlaylist>? TagsInPlaylists { get; set; }
    
    public ICollection<MoodsInPlaylist>? MoodsInPlaylists { get; set; }
    
    public ICollection<TrackInPlaylist>? TrackInPlaylists { get; set; }
}