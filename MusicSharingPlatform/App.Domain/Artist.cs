﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class Artist : IdentityUser
{
    [MaxLength(50)]
    public string DisplayName { get; set; } = default!;
    
    [MaxLength(1000)]
    public string Bio { get; set; } = default!;
    
    [MaxLength(512)]
    public string ProfilePicture { get; set; } = default!;

    public DateTime JoinDate { get; set; }
    
    public ICollection<ArtistInTrack>? ArtistInTracks { get; set; }
    
    public ICollection<Rating>? Ratings { get; set; }
    
    public ICollection<UserSavedTracks>? SavedTracks { get; set; }
    
    public ICollection<UserLink>? UserLinks { get; set; }
    
    public ICollection<Playlist>? Playlists { get; set; }
}