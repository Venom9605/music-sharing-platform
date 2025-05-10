using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DTO.v1;

public class TrackCreate
{
    [Required]
    [MaxLength(100)]
    [Display(Name = "Title", Prompt = "Title", ResourceType = typeof(App.Resources.Domain.Track))]
    public string Title { get; set; } = default!;
    
    [Required]
    [MaxLength(512)]
    [Display(Name = "FilePath", Prompt = "FilePath", ResourceType = typeof(App.Resources.Domain.Track))]
    public string FilePath { get; set; } = default!;
    
    [Required]
    [MaxLength(512)]
    [Display(Name = "CoverPath", Prompt = "CoverPath", ResourceType = typeof(App.Resources.Domain.Track))]
    public string CoverPath { get; set; } = default!;
    
    public ICollection<ArtistInTrackCreate>? ArtistInTracks { get; set; }
    
    public ICollection<TrackLinkCreate>? TrackLinks { get; set; }
    
    public ICollection<TagInTrackCreate>? TagsInTracks { get; set; }
    
    public ICollection<MoodInTrackCreate>? MoodsInTracks { get; set; }
}