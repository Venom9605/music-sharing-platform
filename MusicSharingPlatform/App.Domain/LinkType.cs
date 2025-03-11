using System.ComponentModel.DataAnnotations;

namespace Domain;

public class LinkType : BaseEntity
{
    [MaxLength(100)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.LinkType))]
    public string Name { get; set; } = default!;
    
    public ICollection<UserLink>? UserLinks { get; set; }
    
    public ICollection<TrackLink>? TrackLinks { get; set; }
}