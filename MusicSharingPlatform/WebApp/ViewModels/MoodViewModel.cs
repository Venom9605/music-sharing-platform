namespace WebApp.ViewModels;

public class MoodViewModel
{
    public string Name { get; set; } = default!;
}

public class MoodCreateViewModel : MoodViewModel
{
}

public class MoodEditViewModel : MoodViewModel
{
    public Guid Id { get; set; } = default!;
}