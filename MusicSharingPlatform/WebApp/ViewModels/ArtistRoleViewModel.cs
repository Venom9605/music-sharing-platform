
namespace WebApp.ViewModels;

public class ArtistRoleViewModel
{
    public string Name { get; set; } = default!;
}


public class ArtistRoleCreateViewModel : ArtistRoleViewModel
{
}

public class ArtistRoleEditViewModel : ArtistRoleViewModel
{
    public Guid Id { get; set; } = default!;
}