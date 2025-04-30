using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class MoodsInTrack : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Mood), Prompt = nameof(Mood), ResourceType = typeof(App.Resources.Domain.MoodsInTrack))]
    public Guid MoodId { get; set; }
    [Display(Name = nameof(Mood), Prompt = nameof(Mood), ResourceType = typeof(App.Resources.Domain.MoodsInTrack))]
    public Mood? Mood { get; set; }
    
    
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.MoodsInTrack))]
    public Guid TrackId { get; set; }
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.MoodsInTrack))]
    public Track? Track { get; set; }
}