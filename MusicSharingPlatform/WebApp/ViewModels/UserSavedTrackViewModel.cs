using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class UserSavedTrackViewModel
{
    public UserSavedTracks UserSavedTrack { get; set; } = default!;
    
    [ValidateNever]
    public SelectList TracksList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList UsersList { get; set; } = default!;
}