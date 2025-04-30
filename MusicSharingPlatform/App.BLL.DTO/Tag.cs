using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class Tag : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    [MaxLength(100)]   
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Tag))]
    public string Name { get; set; } = default!;
    
    public ICollection<TagsInTrack>? TagsInTracks { get; set; }
    
    public ICollection<TagsInPlaylist>? TagsInPlaylists { get; set; }
}