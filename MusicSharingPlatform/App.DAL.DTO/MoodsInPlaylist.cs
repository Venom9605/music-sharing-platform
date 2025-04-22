using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DAL.DTO;

public class MoodsInPlaylist : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Mood), Prompt = nameof(Mood), ResourceType = typeof(App.Resources.Domain.MoodsInPlaylist))]
    public Guid MoodId { get; set; }
    [Display(Name = nameof(Mood), Prompt = nameof(Mood), ResourceType = typeof(App.Resources.Domain.MoodsInPlaylist))]
    public Mood? Mood { get; set; }
    
    
    [Display(Name = nameof(Playlist), Prompt = nameof(Playlist), ResourceType = typeof(App.Resources.Domain.MoodsInPlaylist))]
    public Guid PlaylistId { get; set; }
    [Display(Name = nameof(Playlist), Prompt = nameof(Playlist), ResourceType = typeof(App.Resources.Domain.MoodsInPlaylist))]
    public Playlist? Playlist { get; set; }
}