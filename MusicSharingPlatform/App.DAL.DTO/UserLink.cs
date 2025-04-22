using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DAL.DTO;

public class UserLink : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.UserLink))]
    public string UserId { get; set; } = default!;
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.UserLink))]
    public Artist? User { get; set; } 
    
    
    [Display(Name = nameof(LinkType), Prompt = nameof(LinkType), ResourceType = typeof(App.Resources.Domain.UserLink))]
    public Guid LinkTypeId { get; set; }
    [Display(Name = nameof(LinkType), Prompt = nameof(LinkType), ResourceType = typeof(App.Resources.Domain.UserLink))]
    public LinkType? LinkType { get; set; }
    
    
    [MaxLength(500)]
    [Display(Name = nameof(Url), Prompt = nameof(Url), ResourceType = typeof(App.Resources.Domain.UserLink))]
    public string Url { get; set; } = default!;
}