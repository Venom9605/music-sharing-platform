using Base.Interfaces;

namespace App.DTO.v1;

public class MoodsInTrack : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    public Guid MoodId { get; set; }

    public Guid TrackId { get; set; }

    public string? MoodName { get; set; } = default!;
    
}