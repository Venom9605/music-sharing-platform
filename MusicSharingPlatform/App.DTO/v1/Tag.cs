using Base.Interfaces;

namespace App.DTO.v1;

public class Tag : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
}