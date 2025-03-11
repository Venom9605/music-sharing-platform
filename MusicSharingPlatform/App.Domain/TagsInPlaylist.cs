using System.ComponentModel.DataAnnotations;

namespace Domain;

public class TagsInPlaylist : BaseEntity
{
    [Display(Name = nameof(Playlist), Prompt = nameof(Playlist), ResourceType = typeof(App.Resources.Domain.TagsInPlaylist))]
    public Guid PlaylistId { get; set; }
    [Display(Name = nameof(Playlist), Prompt = nameof(Playlist), ResourceType = typeof(App.Resources.Domain.TagsInPlaylist))]
    public Playlist? Playlist { get; set; }
    
    
    [Display(Name = nameof(Tag), Prompt = nameof(Tag), ResourceType = typeof(App.Resources.Domain.TagsInPlaylist))]
    public Guid TagId { get; set; }
    [Display(Name = nameof(Tag), Prompt = nameof(Tag), ResourceType = typeof(App.Resources.Domain.TagsInPlaylist))]
    public Tag? Tag { get; set; }
}