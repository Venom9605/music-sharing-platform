using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace App.DTO.v1;

public class FileUploadDto
{
    [Required]
    public IFormFile File { get; set; } = default!;
}