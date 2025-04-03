using Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class ArtistInTrackViewModel
{
    public ArtistInTrack ArtistInTrack { get; set; } = default!;
    
    [ValidateNever]
    public SelectList ArtistsList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList ArtistRolesList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList TracksList { get; set; } = default!;
}