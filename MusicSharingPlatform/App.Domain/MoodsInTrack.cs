﻿using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain;

public class MoodsInTrack : BaseEntity
{
    [Display(Name = nameof(Mood), Prompt = nameof(Mood), ResourceType = typeof(App.Resources.Domain.MoodsInTrack))]
    public Guid MoodId { get; set; }
    [Display(Name = nameof(Mood), Prompt = nameof(Mood), ResourceType = typeof(App.Resources.Domain.MoodsInTrack))]
    public Mood? Mood { get; set; }
    
    
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.MoodsInTrack))]
    public Guid TrackId { get; set; }
    [Display(Name = nameof(Track), Prompt = nameof(Track), ResourceType = typeof(App.Resources.Domain.MoodsInTrack))]
    public Track? Track { get; set; }
}