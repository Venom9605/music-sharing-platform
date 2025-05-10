using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;
using Base.Interfaces;

namespace App.BLL.DTO;

public class ArtistInTrack : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }

    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.ArtistInTrack))]
    public Guid TrackId { get; set; }
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.ArtistInTrack))]
    public Track? Track { get; set; }
    

    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.ArtistInTrack))]
    public string UserId { get; set; } = default!;
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.ArtistInTrack))]
    public Artist? User { get; set; } 
    
    
    [Display(Name = nameof(ArtistRole), Prompt = nameof(ArtistRole), ResourceType = typeof(App.Resources.Domain.ArtistInTrack))]
    public Guid ArtistRoleId { get; set; }
    [Display(Name = nameof(ArtistRole), Prompt = nameof(ArtistRole), ResourceType = typeof(App.Resources.Domain.ArtistInTrack))]
    public ArtistRole? ArtistRole { get; set; }
    
    
    public string? ArtistDisplayName { get; set; } = default!;
    
    public string? ArtistRoleName { get; set; } = default!;
}