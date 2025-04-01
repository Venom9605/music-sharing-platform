using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain;

public class Tag : BaseEntity
{
    [MaxLength(100)]   
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Tag))]
    public string Name { get; set; } = default!;
    
    public ICollection<TagsInTrack>? TagsInTracks { get; set; }
    
    public ICollection<TagsInPlaylist>? TagsInPlaylists { get; set; }
}