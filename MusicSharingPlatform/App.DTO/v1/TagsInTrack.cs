using Base.Interfaces;

namespace App.DTO.v1;

public class TagsInTrack : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    public Guid TrackId { get; set; }

    public Guid TagId { get; set; }
    
    public string? TagName { get; set; } = default!;

}