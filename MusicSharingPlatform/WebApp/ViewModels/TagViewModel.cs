namespace WebApp.ViewModels;

public class TagViewModel
{
    public string Name { get; set; } = default!;
}

public class TagCreateViewModel : TagViewModel
{
}

public class TagEditViewModel : TagViewModel
{
    public Guid Id { get; set; } = default!;
}