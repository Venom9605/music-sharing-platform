using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext
{
    public DbSet<Artist> Artists { get; set; } = default!;
    public DbSet<ArtistRole> ArtistRoles { get; set; } = default!;
    public DbSet<ArtistInTrack> ArtistsInTracks { get; set; } = default!;
    
    public DbSet<Mood> Moods { get; set; } = default!;
    public DbSet<MoodsInTrack> MoodsInTracks { get; set; } = default!;
    public DbSet<MoodsInPlaylist> MoodsInPlaylists { get; set; } = default!;
    
    public DbSet<Tag> Tags { get; set; } = default!;
    public DbSet<TagsInTrack> TagsInTracks { get; set; } = default!;
    public DbSet<TagsInPlaylist> TagsInPlaylists { get; set; } = default!;
    
    public DbSet<Track> Tracks { get; set; } = default!;
    public DbSet<Rating> Ratings { get; set; } = default!;
    public DbSet<Playlist> Playlists { get; set; } = default!;
    public DbSet<TrackInPlaylist> TracksInPlaylists { get; set; } = default!;
    
    public DbSet<LinkType> LinkTypes { get; set; } = default!;
    public DbSet<TrackLink> TrackLinks { get; set; } = default!;
    public DbSet<UserLink> UserLinks { get; set; } = default!;
    public DbSet<UserSavedTracks> UserSavedTracks { get; set; } = default!;
    
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}