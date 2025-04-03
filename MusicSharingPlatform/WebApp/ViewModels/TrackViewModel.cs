using System.ComponentModel.DataAnnotations;
using Domain;

namespace WebApp.ViewModels;

public class TrackViewModel
{
    [MaxLength(100)]
    public string Title { get; set; } = default!;
    
    [MaxLength(512)]
    public string FilePath { get; set; } = default!;
    
    [MaxLength(512)]
    public string CoverPath { get; set; } = default!;

    public int Duration { get; set; }
}

public class TrackCreateViewModel : TrackViewModel
{
}

public class TrackEditViewModel : TrackViewModel
{
    public Guid Id { get; set; } = default!;
}
