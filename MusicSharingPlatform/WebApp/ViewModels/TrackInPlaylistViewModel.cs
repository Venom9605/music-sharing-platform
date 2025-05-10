using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class TrackInPlaylistViewModel
{
    public TrackInPlaylist TrackInPlaylist { get; set; } = default!;
    
    [ValidateNever]
    public SelectList TracksList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList PlaylistsList { get; set; } = default!;
}