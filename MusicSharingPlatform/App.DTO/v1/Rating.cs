using Base.Interfaces;

namespace App.DTO.v1;
public class Rating : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    public Guid TrackId { get; set; }
    
    public string UserId { get; set; } = default!;
    
    public int Score { get; set; }
    
    public string? Comment { get; set; } = default!;
    
    public string? ArtistDisplayName { get; set; } = default!;
}
