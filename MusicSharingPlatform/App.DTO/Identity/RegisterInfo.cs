using System.ComponentModel.DataAnnotations;

namespace App.DTO.Identity;

public class RegisterInfo
{
    [MaxLength(128)]
    public string Email { get; set; } = default!;
    [MaxLength(128)]
    public string Password { get; set; } = default!;
    [MaxLength(128)] 
    public string DisplayName { get; set; } = default!;
    [MaxLength(128)]
    public string Bio { get; set; } = default!;
    [MaxLength(128)] 
    public string ProfilePicture { get; set; } = default!;
    
}