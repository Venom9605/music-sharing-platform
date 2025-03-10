using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class UserLink : BaseEntity
{
    public string UserId { get; set; } = default!;
    public IdentityUser? User { get; set; } 
    
    public Guid LinkTypeId { get; set; }
    public LinkType? LinkType { get; set; }
    
    [MaxLength(500)]
    public string Url { get; set; } = default!;
}