namespace WebApp.ViewModels;

public class ArtistViewModel
{
    public string DisplayName { get; set; } = default!;
    
    public string? Bio { get; set; } = default!;
    
    public string? ProfilePicture { get; set; } = default!;
}

public class ArtistEditViewModel : ArtistViewModel
{
    public string Id { get; set; } = default!;
}