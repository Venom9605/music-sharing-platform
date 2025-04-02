using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Base.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class Rating : BaseEntity, IDomainUserId
{
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.Rating))]
    public Guid TrackId { get; set; }
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.Rating))]
    public Track? Track { get; set; }
    
    
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.Rating))]
    public string UserId { get; set; } = default!;
    public Artist? User { get; set; } 
    
    
    [Display(Name = nameof(Score), Prompt = nameof(Score), ResourceType = typeof(App.Resources.Domain.Rating))]
    public int Score { get; set; }
    
    
    [MaxLength(512)]
    [Display(Name = nameof(Comment), Prompt = nameof(Comment), ResourceType = typeof(App.Resources.Domain.Rating))]
    public string? Comment { get; set; } = default!;

    
    private DateTime _date;
    
    [Display(Name = nameof(Date), Prompt = nameof(Date), ResourceType = typeof(App.Resources.Domain.Rating))]
    public DateTime Date
    {
        get => _date;
        set => _date = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }   
    
}