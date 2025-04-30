using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class TrackInPlaylist : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.TrackInPlaylist))]
    public Guid TrackId { get; set; }
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.TrackInPlaylist))]
    public Track? Track { get; set; }
    
    
    [Display(Name = nameof(Playlist), Prompt = nameof(Playlist), ResourceType = typeof(App.Resources.Domain.TrackInPlaylist))]
    public Guid PlaylistId { get; set; }
    [Display(Name = nameof(Playlist), Prompt = nameof(Playlist), ResourceType = typeof(App.Resources.Domain.TrackInPlaylist))]
    public Playlist? Playlist { get; set; }
}