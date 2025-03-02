using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Track : BaseEntity
{
    [MaxLength(100)]
    public string Title { get; set; } = default!;
    
    [MaxLength(512)]
    public string FilePath { get; set; } = default!;
    
    [MaxLength(512)]
    public string CoverPath { get; set; } = default!;
    
    public DateTime Uploaded { get; set; }
    
    public int Duration { get; set; }
    
    public int TimesPlayed { get; set; }
    
    public int TimesSaved { get; set; }
    
    
    public ICollection<ArtistInTrack>? ArtistInTracks { get; set; }
    
    public ICollection<Rating>? Rating { get; set; }
    
    public ICollection<UserSavedTracks>? SavedByUsers { get; set; }
    
    public ICollection<TrackLink>? TrackLinks { get; set; }
    
    public ICollection<TagsInTrack>? TagsInTracks { get; set; }
    
    public ICollection<MoodsInTrack>? MoodsInTracks { get; set; }
    
    public ICollection<TrackInPlaylist>? TrackInPlaylists { get; set; }
}