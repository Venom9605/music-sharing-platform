using System.ComponentModel.DataAnnotations;

namespace Domain;

public class MoodsInPlaylist : BaseEntity
{
    [Display(Name = nameof(Mood), Prompt = nameof(Mood), ResourceType = typeof(App.Resources.Domain.MoodsInPlaylist))]
    public Guid MoodId { get; set; }
    [Display(Name = nameof(Mood), Prompt = nameof(Mood), ResourceType = typeof(App.Resources.Domain.MoodsInPlaylist))]
    public Mood? Mood { get; set; }
    
    
    [Display(Name = nameof(Playlist), Prompt = nameof(Playlist), ResourceType = typeof(App.Resources.Domain.MoodsInPlaylist))]
    public Guid PlaylistId { get; set; }
    [Display(Name = nameof(Playlist), Prompt = nameof(Playlist), ResourceType = typeof(App.Resources.Domain.MoodsInPlaylist))]
    public Playlist? Playlist { get; set; }
}