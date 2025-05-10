using Base.Interfaces;

namespace App.DTO.v1;

public class UserSavedTracks : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    public Guid TrackId { get; set; }
    public string UserId { get; set; } = default!;
}
