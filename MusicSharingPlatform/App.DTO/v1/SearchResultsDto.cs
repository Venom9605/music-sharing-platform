namespace App.DTO.v1;

public class SearchResultsDto
{
    public List<Track> Tracks { get; set; } = new();
    public List<Artist> Artists { get; set; } = new();
}