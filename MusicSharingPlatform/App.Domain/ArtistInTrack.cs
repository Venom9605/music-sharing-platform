﻿using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class ArtistInTrack : BaseEntity
{
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.ArtistInTrack))]
    public Guid TrackId { get; set; }
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.ArtistInTrack))]
    public Track? Track { get; set; }
    

    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.ArtistInTrack))]
    public string UserId { get; set; } = default!;
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.ArtistInTrack))]
    public IdentityUser? User { get; set; } 
    
    
    [Display(Name = nameof(ArtistRole), Prompt = nameof(ArtistRole), ResourceType = typeof(App.Resources.Domain.ArtistInTrack))]
    public Guid ArtistRoleId { get; set; }
    [Display(Name = nameof(ArtistRole), Prompt = nameof(ArtistRole), ResourceType = typeof(App.Resources.Domain.ArtistInTrack))]
    public ArtistRole? ArtistRole { get; set; }
}