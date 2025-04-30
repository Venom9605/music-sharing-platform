using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class MoodsInPlaylistViewModel
{
    public MoodsInPlaylist MoodsInPlaylist { get; set; } = default!;
    
    [ValidateNever]
    public SelectList MoodsList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList PlaylistsList { get; set; } = default!;
}