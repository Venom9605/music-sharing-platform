using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DAL.DTO;

public class TagsInPlaylist : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Playlist), Prompt = nameof(Playlist), ResourceType = typeof(App.Resources.Domain.TagsInPlaylist))]
    public Guid PlaylistId { get; set; }
    [Display(Name = nameof(Playlist), Prompt = nameof(Playlist), ResourceType = typeof(App.Resources.Domain.TagsInPlaylist))]
    public Playlist? Playlist { get; set; }
    
    
    [Display(Name = nameof(Tag), Prompt = nameof(Tag), ResourceType = typeof(App.Resources.Domain.TagsInPlaylist))]
    public Guid TagId { get; set; }
    [Display(Name = nameof(Tag), Prompt = nameof(Tag), ResourceType = typeof(App.Resources.Domain.TagsInPlaylist))]
    public Tag? Tag { get; set; }
}