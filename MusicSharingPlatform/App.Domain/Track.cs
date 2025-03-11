using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Track : BaseEntity
{
    [MaxLength(100)]
    [Display(Name = nameof(Title), Prompt = nameof(Title), ResourceType = typeof(App.Resources.Domain.Track))]
    public string Title { get; set; } = default!;
    
    
    [MaxLength(512)]
    [Display(Name = nameof(FilePath), Prompt = nameof(FilePath), ResourceType = typeof(App.Resources.Domain.Track))]
    public string FilePath { get; set; } = default!;
    
    
    [MaxLength(512)]
    [Display(Name = nameof(CoverPath), Prompt = nameof(CoverPath), ResourceType = typeof(App.Resources.Domain.Track))]
    public string CoverPath { get; set; } = default!;
    
    
    private DateTime _uploaded;
    
    [Display(Name = nameof(Uploaded), Prompt = nameof(Uploaded), ResourceType = typeof(App.Resources.Domain.Track))]
    public DateTime Uploaded
    {
        get => _uploaded;
        set => _uploaded = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
    
    
    [Display(Name = nameof(Duration), Prompt = nameof(Duration), ResourceType = typeof(App.Resources.Domain.Track))]
    public int Duration { get; set; }
    
    
    [Display(Name = nameof(TimesPlayed), Prompt = nameof(TimesPlayed), ResourceType = typeof(App.Resources.Domain.Track))]
    public int TimesPlayed { get; set; }
    
    
    [Display(Name = nameof(TimesSaved), Prompt = nameof(TimesSaved), ResourceType = typeof(App.Resources.Domain.Track))]
    public int TimesSaved { get; set; }
    
    
    public ICollection<ArtistInTrack>? ArtistInTracks { get; set; }
    
    public ICollection<Rating>? Rating { get; set; }
    
    public ICollection<UserSavedTracks>? SavedByUsers { get; set; }
    
    public ICollection<TrackLink>? TrackLinks { get; set; }
    
    public ICollection<TagsInTrack>? TagsInTracks { get; set; }
    
    public ICollection<MoodsInTrack>? MoodsInTracks { get; set; }
    
    public ICollection<TrackInPlaylist>? TrackInPlaylists { get; set; }
}