using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain;

public class TagsInTrack : BaseEntity
{
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.TagsInTrack))]
    public Guid TrackId { get; set; }
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.TagsInTrack))]
    public Track? Track { get; set; }
    
    
    [Display(Name = nameof(Tag), Prompt = nameof(Tag), ResourceType = typeof(App.Resources.Domain.TagsInTrack))]
    public Guid TagId { get; set; }
    [Display(Name = nameof(Tag), Prompt = nameof(Tag), ResourceType = typeof(App.Resources.Domain.TagsInTrack))]
    public Tag? Tag { get; set; }
}