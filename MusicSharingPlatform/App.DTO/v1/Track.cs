using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DTO.v1;

public class Track : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    [MaxLength(100)]
    [Display(Name = "Title", Prompt = "Title", ResourceType = typeof(App.Resources.Domain.Track))]
    public string Title { get; set; } = default!;
    
    
    [MaxLength(512)]
    [Display(Name = "FilePath", Prompt = "FilePath", ResourceType = typeof(App.Resources.Domain.Track))]
    public string FilePath { get; set; } = default!;
    
    
    [MaxLength(512)]
    [Display(Name = "CoverPath", Prompt = "CoverPath", ResourceType = typeof(App.Resources.Domain.Track))]
    public string CoverPath { get; set; } = default!;
    
    
    [Display(Name = "Duration", Prompt = "Duration", ResourceType = typeof(App.Resources.Domain.Track))]
    public int Duration { get; set; }
    
    
    [Display(Name = "TimesPlayed", Prompt = "TimesPlayed", ResourceType = typeof(App.Resources.Domain.Track))]
    public int TimesPlayed { get; set; }
    
    
    [Display(Name = "TimesSaved", Prompt = "TimesSaved", ResourceType = typeof(App.Resources.Domain.Track))]
    public int TimesSaved { get; set; }
    
    public ICollection<ArtistInTrack>? ArtistInTracks { get; set; }
    
    public ICollection<Rating>? Rating { get; set; }
    
    public ICollection<TrackLink>? TrackLinks { get; set; }
    
    public ICollection<TagsInTrack>? TagsInTracks { get; set; }
    
    public ICollection<MoodsInTrack>? MoodsInTracks { get; set; }
    
}