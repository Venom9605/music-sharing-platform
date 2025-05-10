using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class TrackEdit
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    [Display(Name = "Title", Prompt = "Title", ResourceType = typeof(App.Resources.Domain.Track))]
    public string Title { get; set; } = default!;
    
    [Required]
    [MaxLength(512)]
    [Display(Name = "CoverPath", Prompt = "CoverPath", ResourceType = typeof(App.Resources.Domain.Track))]
    public string CoverPath { get; set; } = default!;
    
    public ICollection<ArtistInTrackCreate>? ArtistInTracks { get; set; }
    
    public ICollection<TrackLinkCreate>? TrackLinks { get; set; }
    
    public ICollection<TagInTrackCreate>? TagsInTracks { get; set; }
    
    public ICollection<MoodInTrackCreate>? MoodsInTracks { get; set; }
}