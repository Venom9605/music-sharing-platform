using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class RatingsViewModel
{
    
    public Rating Rating { get; set; } = default!;
    
    [ValidateNever]
    public SelectList TracksList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList ArtistsList { get; set; } = default!;
}