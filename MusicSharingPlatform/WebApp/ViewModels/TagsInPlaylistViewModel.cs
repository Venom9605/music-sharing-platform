using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class TagsInPlaylistViewModel
{
    public TagsInPlaylist TagsInPlaylist { get; set; } = default!;
    
    [ValidateNever]
    public SelectList TagsList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList PlaylistsList { get; set; } = default!;
}