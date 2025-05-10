using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.BLL.DTO;

namespace WebApp.ViewModels;

public class PlaylistViewModel
{
    public Playlist Playlist { get; set; } = default!;
    
    [ValidateNever]
    public SelectList ArtistsList { get; set; } = default!;
    
}