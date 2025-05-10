using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class RatingCreate
{
    [Required]
    public Guid TrackId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Score { get; set; }

    [MaxLength(512)]
    public string? Comment { get; set; }
}