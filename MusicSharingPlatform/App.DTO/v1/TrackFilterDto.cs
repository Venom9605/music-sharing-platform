namespace App.DTO.v1;

public class TrackFilterDto
{
    public List<Guid> TagIds { get; set; } = new();
    public List<Guid> MoodIds { get; set; } = new();
}