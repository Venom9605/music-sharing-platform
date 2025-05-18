using App.BLL.Interfaces;
using App.DTO.v1;
using App.DTO.v1.Mappers;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// Artist API Controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ArtistController : ControllerBase
{
    
    private readonly IAppBLL _bll;
    private readonly ArtistMapper _mapper = new ArtistMapper();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bll"></param>
    public ArtistController(IAppBLL bll)
    {
        _bll = bll;
    }

    /// <summary>
    /// Retrieves information about the currently authenticated user.
    /// </summary>
    /// <returns>The current artist's profile information.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(App.DTO.v1.Artist), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<App.DTO.v1.Artist>> GetUserInfo()
    {
        var artist = await _bll.ArtistService.FindAsync(User.GetUserId());

        return Ok(artist);
    }
    
    /// <summary>
    /// Retrieves information about a specific artist by their user ID.
    /// </summary>
    /// <param name="userId">The user ID of the artist.</param>
    /// <returns>Artist profile information or 404 if not found.</returns>
    [HttpGet("{userId}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(App.DTO.v1.Artist), 200)]
    [ProducesResponseType(404)]
    [AllowAnonymous]
    public async Task<ActionResult<App.DTO.v1.Artist>> GetUserInfoById(string userId)
    {
        var artist = await _bll.ArtistService.FindAsync(userId);

        return Ok(artist);
    }
    
    /// <summary>
    /// Retrieves the most popular artist (based on ratings, saves, or play count).
    /// </summary>
    /// <returns>Most popular artist or 404 if none available.</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(App.DTO.v1.Artist), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<App.DTO.v1.Artist>> GetMostPopular()
    {
        var artist = await _bll.ArtistService.GetMostPopularArtistAsync();
        if (artist == null) return NotFound();

        return Ok(_mapper.Map(artist));
    }
    
    /// <summary>
    /// Edits the current authenticated artist's profile.
    /// </summary>
    /// <param name="dto">DTO containing updated profile fields.</param>
    /// <returns>NoContent if update is successful, or 404 if artist not found.</returns>
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
    
    /// <summary>
    /// Uploads a profile picture to the server and returns the relative file path.
    /// </summary>
    /// <param name="dto">File upload payload (multipart/form-data).</param>
    /// <returns>Relative path to uploaded image or error message.</returns>
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