using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class LinkType : IBaseEntityId<Guid>
{
    public Guid Id { get; set; }
    
    [MaxLength(100)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.LinkType))]
    public string Name { get; set; } = default!;
    
    public ICollection<UserLink>? UserLinks { get; set; }
    
    public ICollection<TrackLink>? TrackLinks { get; set; }
}