using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class Rating : BaseEntity
{
    public Guid TrackId { get; set; }
    public Track? Track { get; set; }
    
    public string UserId { get; set; } = default!;
    public IdentityUser? User { get; set; } 
    
    public int Score { get; set; }
    
    [MaxLength(512)]
    public string? Comment { get; set; } = default!;
    
    public DateTime Date { get; set; }
    
}