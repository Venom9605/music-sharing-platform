using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class TagsInTrackViewModel
{
    public TagsInTrack TagsInTrack { get; set; } = default!;
    
    [ValidateNever]
    public SelectList TagsList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList TracksList { get; set; } = default!;
}