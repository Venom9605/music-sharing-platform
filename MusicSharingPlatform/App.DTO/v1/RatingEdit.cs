using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class RatingEdit
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [Range(1, 5)]
    public int Score { get; set; }

    [MaxLength(512)]
    public string? Comment { get; set; }
}