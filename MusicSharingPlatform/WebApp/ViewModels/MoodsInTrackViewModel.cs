using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class MoodsInTrackViewModel
{
    public MoodsInTrack MoodsInTrack { get; set; } = default!;
    
    [ValidateNever]
    public SelectList MoodsList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList TracksList { get; set; } = default!;
}