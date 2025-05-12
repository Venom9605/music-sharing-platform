using Base.Interfaces;

namespace App.DTO.v1;

public class ArtistRole : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}