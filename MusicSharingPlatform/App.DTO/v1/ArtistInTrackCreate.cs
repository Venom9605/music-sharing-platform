namespace App.DTO.v1;

public class ArtistInTrackCreate
{
    public string UserId { get; set; } = default!;
    
    public Guid ArtistRoleId { get; set; }
}