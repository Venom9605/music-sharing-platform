namespace WebApp.ViewModels;

public class LinkTypeViewModel
{
    public string Name { get; set; } = default!;
}

public class LinkTypeCreateViewModel : LinkTypeViewModel
{
}

public class LinkTypeEditViewModel : LinkTypeViewModel
{
    public Guid Id { get; set; } = default!;
}