using App.BLL.Interfaces;
using App.DTO.v1;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// Track API controller
/// </summary>
[ApiVersion( "1.0" )]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TrackController : ControllerBase
{
    private readonly IAppBLL _bll;
    
    private readonly App.DTO.v1.Mappers.TrackMapper _mapper =
        new App.DTO.v1.Mappers.TrackMapper();


    /// <summary>
    /// Track controller constructor
    /// </summary>
    /// <param name="bll"></param>
    public TrackController(IAppBLL bll)
    {
        _bll = bll;
    }
    
    /// <summary>
    /// Get all tracks for the currently logged-in user
    /// </summary>
    /// <returns>List of tracks belonging to the user</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.Track>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<App.DTO.v1.Track>>> GetTracks()
    {
        var data = (await _bll.TrackService.AllAsync(User.GetUserId())).ToList();
        
        var res = data.Select(t => _mapper.Map(t)!).ToList();
    
        return res;
    }
    
    /// <summary>
    /// Get a specific track by ID for the currently logged-in user
    /// </summary>
    /// <param name="id">ID of the track</param>
    /// <returns>The requested track if found, or a 404 error if not.</returns>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(App.DTO.v1.Track), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<App.DTO.v1.Track>> GetTrack(Guid id)
    {
        var track = await _bll.TrackService.FindAsync(id, User.GetUserId());
        
        if (track == null)
        {
            return NotFound();
        }
        
        return _mapper.Map(track)!;
    }
    
    
    /// <summary>
    /// Get a random track.
    /// </summary>
    /// <returns>The requested track if found, or a 404 error if not.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(App.DTO.v1.Track), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<App.DTO.v1.Track>> GetRandomTrack()
    {
        var track = await _bll.TrackService.GetRandomTrackAsync();
        
        if (track == null)
        {
            return NotFound();
        }
        
        return Ok(_mapper.Map(track));
    }
    
    
    
    /// <summary>
    /// Create a new track for the currently logged-in user
    /// </summary>
    /// <param name="track">The track data to create.</param>
    /// <returns>The created track with its ID and other generated properties.</returns>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.TrackCreate>), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<App.DTO.v1.Track>> CreateTrack(App.DTO.v1.TrackCreate track)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        
        var userId = User.GetUserId();
        
        
        if (track.Collaborators != null && track.Collaborators.Any())
        {
            var resolved = new List<App.DTO.v1.ArtistInTrackCreate>();

            foreach (var collaborator in track.Collaborators)
            {
                var normalized = collaborator.Email.Trim().ToUpperInvariant();
                
                var artist = await _bll.ArtistService.FindByNormalizedUserNameAsync(normalized);
                if (artist != null)
                {
                    resolved.Add(new App.DTO.v1.ArtistInTrackCreate
                    {
                        UserId = artist.Id,
                        ArtistRoleId = collaborator.ArtistRoleId
                    });
                }
            }

            track.ArtistInTracks ??= new List<App.DTO.v1.ArtistInTrackCreate>();
            track.ArtistInTracks = track.ArtistInTracks.Concat(resolved).ToList();
        }
        
        
        var bllEntity = _mapper.Map(track, userId)!;
        
        _bll.TrackService.Add(bllEntity);
        await _bll.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTrack), new
        {
            id = bllEntity.Id,
            version = HttpContext.GetRequestedApiVersion()!.ToString()
        }, bllEntity);
    }
    
    /// <summary>
    /// Delete a track for the currently logged-in user
    /// </summary>
    /// <param name="id">ID of the track to delete</param>
    /// <returns>No content if successful, or an error response</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteTrack(Guid id)
    {
        var trackExists = await _bll.TrackService.ExistsAsync(id, User.GetUserId());
        if (!trackExists)
        {
            return NotFound();
        }
        
        await _bll.TrackService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();

        return NoContent();
    }
    
    /// <summary>
    /// Upload a cover image for a track
    /// </summary>
    /// <param name="file">Image file</param>
    /// <returns>Relative path to the uploaded image</returns>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UploadCover([FromForm] FileUploadDto dto)
    {
        var file = dto.File;
        
        if (file == null)
        {
            return BadRequest("No file uploaded.");
        }



        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "covers");
        Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var relativePath = $"covers/{uniqueFileName}";


        return Ok(new { path = relativePath });
    }
    
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UploadTrackFile([FromForm] FileUploadDto dto)
    {
        var file = dto.File;
        
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var relativePath = $"uploads/{uniqueFileName}";
        return Ok(new { path = relativePath });
    }


}