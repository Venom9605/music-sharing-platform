using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class TrackLinkViewModel
{
    public TrackLink TrackLink { get; set; } = default!;
    
    [ValidateNever]
    public SelectList TracksList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList LinkTypesList { get; set; } = default!;
}