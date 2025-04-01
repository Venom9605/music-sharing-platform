using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class UserSavedTracks : BaseEntity
{
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.UserSavedTracks))]
    public Guid TrackId { get; set; }
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.UserSavedTracks))]
    public Track? Track { get; set; }

    
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.UserSavedTracks))]
    public string UserId { get; set; } = default!;
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.UserSavedTracks))]
    public Artist? User { get; set; } 

    
    private DateTime _savedAt;
    [Display(Name = nameof(SavedAt), Prompt = nameof(SavedAt), ResourceType = typeof(App.Resources.Domain.UserSavedTracks))]
    public DateTime SavedAt 
    {
        get => _savedAt;
        set => _savedAt = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
}