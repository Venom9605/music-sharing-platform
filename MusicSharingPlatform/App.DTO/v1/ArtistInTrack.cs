using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DTO.v1;

public class ArtistInTrack : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    public Guid TrackId { get; set; }
    
    public string UserId { get; set; } = default!;
    
    public Guid ArtistRoleId { get; set; }
    
    public string? ArtistDisplayName { get; set; }
    
    public string? ArtistRoleName { get; set; }
}