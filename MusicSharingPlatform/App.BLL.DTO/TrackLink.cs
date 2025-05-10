using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class TrackLink : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.TrackLink))]
    public Guid TrackId { get; set; }
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.TrackLink))]
    public Track? Track { get; set; }
    
    
    [Display(Name = nameof(LinkType), Prompt = nameof(LinkType), ResourceType = typeof(App.Resources.Domain.TrackLink))]
    public Guid LinkTypeId { get; set; }
    [Display(Name = nameof(LinkType), Prompt = nameof(LinkType), ResourceType = typeof(App.Resources.Domain.TrackLink))]
    public LinkType? LinkType { get; set; }
    
    
    [MaxLength(500)]
    [Display(Name = nameof(Url), Prompt = nameof(Url), ResourceType = typeof(App.Resources.Domain.TrackLink))]
    public string Url { get; set; } = default!;
    
    public string? LinkTypeName { get; set; } = default!;
}