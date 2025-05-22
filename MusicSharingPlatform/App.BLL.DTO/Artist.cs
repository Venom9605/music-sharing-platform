using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;
using Base.Interfaces;

namespace App.BLL.DTO;

public class Artist : IBaseEntityId<string>
{
    public string Id { get; set; } = default!;
    
    [MaxLength(50)]
    [Display(Name = nameof(DisplayName), Prompt = nameof(DisplayName), ResourceType = typeof(App.Resources.Domain.Artist))]
    public string DisplayName { get; set; } = default!;
    
    [MaxLength(1000)]
    [Display(Name = nameof(Bio), Prompt = nameof(Bio), ResourceType = typeof(App.Resources.Domain.Artist))]
    public string? Bio { get; set; } = default!;
    
    [MaxLength(512)]
    [Display(Name = nameof(ProfilePicture), Prompt = nameof(ProfilePicture), ResourceType = typeof(App.Resources.Domain.Artist))]
    public string? ProfilePicture { get; set; } = default!;

    [Display(Name = nameof(JoinDate), Prompt = nameof(JoinDate), ResourceType = typeof(App.Resources.Domain.Artist))]
    public DateTime? JoinDate { get; set; }
    
    public ICollection<ArtistInTrack>? ArtistInTracks { get; set; }
    
    public ICollection<Rating>? Ratings { get; set; }
    
    public ICollection<UserSavedTracks>? SavedTracks { get; set; }
    
    public ICollection<UserLink>? UserLinks { get; set; }
    
    public ICollection<Playlist>? Playlists { get; set; }
    
    
    public bool IsAdmin { get; set; }
}