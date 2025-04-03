using System.ComponentModel.DataAnnotations;
using Domain;

namespace WebApp.ViewModels;

public class TrackViewModel
{
    public string Title { get; set; } = default!;
    
    public string FilePath { get; set; } = default!;
    
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
