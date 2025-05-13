using App.BLL.Interfaces;
using App.DTO.v1;
using App.DTO.v1.Mappers;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ArtistController : ControllerBase
{
    
    private readonly IAppBLL _bll;
    private readonly ArtistMapper _mapper = new ArtistMapper();

    public ArtistController(IAppBLL bll)
    {
        _bll = bll;
    }
    
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(App.DTO.v1.Artist), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<App.DTO.v1.Artist>> GetUserInfo()
    {
        var artist = await _bll.ArtistService.FindAsync(User.GetUserId());

        return Ok(artist);
    }
    
    [HttpPut]
    public async Task<IActionResult> Edit(App.DTO.v1.ArtistEdit dto)
    {
        var userId = User.GetUserId();

        var artist = await _bll.ArtistService.FindTrackedAsync(userId);
        
        if (artist == null)
        {
            return NotFound();
        }
        
        
        artist.DisplayName = dto.DisplayName;
        artist.Bio = dto.Bio;
        artist.ProfilePicture = dto.ProfilePicture;
        
        await _bll.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpPost]
    [AllowAnonymous] // Allow unauthenticated upload before account exists
    [Consumes("multipart/form-data")] 
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UploadProfilePicture([FromForm] FileUploadDto dto)
    {
        var file = dto.File;
        
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile-pictures");
        Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var relativePath = $"profile-pictures/{uniqueFileName}";
        return Ok(new { path = relativePath });
    }
}