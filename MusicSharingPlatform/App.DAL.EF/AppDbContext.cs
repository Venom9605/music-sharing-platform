using Base.Interfaces;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<Artist>
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
    
    
    public DbSet<AppRefreshToken> RefreshTokens { get; set; } = default!;
    
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        
        
        var entries = ChangeTracker.Entries()
            .Where(e => e is {Entity: IDomainMeta});
        
        foreach (var entry in entries) 
        {
            if (entry.State == EntityState.Added)
            {
                (entry.Entity as IDomainMeta)!.CreatedAt = DateTime.UtcNow;
                (entry.Entity as IDomainMeta)!.CreatedBy = "System";
            }
            
            else if (entry.State == EntityState.Modified)
            {
                (entry.Entity as IDomainMeta)!.ChangedAt = DateTime.UtcNow;
                (entry.Entity as IDomainMeta)!.ChangedBy = "System";
                
                entry.Property("CreatedAt").IsModified = false;
                entry.Property("CreatedBy").IsModified = false;
                
            }

        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}