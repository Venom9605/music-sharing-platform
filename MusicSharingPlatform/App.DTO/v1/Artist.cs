using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DTO.v1;

public class Artist : IBaseEntityId<string>
{
    public string Id { get; set; } = default!;
    
    [MaxLength(50)]
    public string DisplayName { get; set; } = default!;
    
    [MaxLength(1000)]
    public string? Bio { get; set; } = default!;
    
    [MaxLength(512)]
    public string? ProfilePicture { get; set; } = default!;

    public DateTime? JoinDate { get; set; }

}