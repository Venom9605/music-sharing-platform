using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DTO.v1;

public class TrackLink : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    public Guid TrackId { get; set; }
    
    public Guid LinkTypeId { get; set; }
    
    public string Url { get; set; } = default!;
    
    public string? LinkTypeName { get; set; } = default!;
}