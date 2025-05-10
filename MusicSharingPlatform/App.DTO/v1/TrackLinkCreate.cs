namespace App.DTO.v1;

public class TrackLinkCreate
{
    public Guid LinkTypeId { get; set; }
    
    public string Url { get; set; } = default!;
}