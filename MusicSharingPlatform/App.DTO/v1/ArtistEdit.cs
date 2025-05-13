using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class ArtistEdit
{
    
    [MaxLength(50)]
    public string DisplayName { get; set; } = default!;
    
    [MaxLength(1000)]
    public string? Bio { get; set; }
    
    [MaxLength(512)]
    public string? ProfilePicture { get; set; }
    
}