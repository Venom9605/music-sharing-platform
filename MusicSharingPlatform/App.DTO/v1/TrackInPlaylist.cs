using Base.Domain;
using Base.Interfaces;

namespace App.DTO.v1;

public class TrackInPlaylist : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    public Guid TrackId { get; set; }
    
    public Guid PlaylistId { get; set; }
    
    public Track? Track { get; set; }
}