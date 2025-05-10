using App.BLL.Interfaces;
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
    [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.Track>), 200)]
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
        
        var bllEntity = _mapper.Map(track)!;
        
        _bll.TrackService.Add(bllEntity);
        await _bll.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTrack), new
        {
            id = bllEntity.Id,
            version = HttpContext.GetRequestedApiVersion()!.ToString()
        }, bllEntity);
    }
    
    /// <summary>
    /// Update an existing track for the currently logged-in user
    /// </summary>
    /// <param name="id">The ID of the track to update.</param>
    /// <param name="dto">The updated track data.</param>
    /// <returns>No content if the update is successful, or an error response if not.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateTrack(Guid id, App.DTO.v1.TrackEdit dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var bllDto = _mapper.Map(dto)!;

        await _bll.TrackService.UpdateTrackWithRelationsAsync(bllDto);
        await _bll.SaveChangesAsync();

        return NoContent();
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


}