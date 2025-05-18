using App.BLL.Interfaces;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;


/// <summary>
/// User saved tracks API controller
/// </summary>
[ApiVersion( "1.0" )]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserSavedTracksController : ControllerBase
{
    private readonly IAppBLL _bll;

    private readonly App.DTO.v1.Mappers.UserSavedTracksMapper _savedTracksMapper =
        new App.DTO.v1.Mappers.UserSavedTracksMapper();
    
    private readonly App.DTO.v1.Mappers.TrackMapper _tracksMapper =
        new App.DTO.v1.Mappers.TrackMapper();


    /// <summary>
    /// User saved tracks controller constructor
    /// </summary>
    /// <param name="bll"></param>
    public UserSavedTracksController(IAppBLL bll)
    {
        _bll = bll;
    }
    
    /// <summary>
    /// Get all saved tracks for the current user
    /// </summary>
    /// <returns>Collection of tracks</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.Track>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<App.DTO.v1.Track>>> GetSavedTracks()
    {
        var userId = User.GetUserId();
        
        var data = await _bll.UserSavedTracksService.GetFullSavedTracksAsync(userId);
        
        var res = data.Select(t => _tracksMapper.Map(t)!).ToList();
    
        return res;
    }
    
    
    /// <summary>
    /// Save a track for the current user.
    /// </summary>
    /// <param name="track">DTO for trackId to be saved.</param>
    /// <returns>Saved entity</returns>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.UserSavedTracks>), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<App.DTO.v1.UserSavedTracks>> AddSavedTrack(App.DTO.v1.UserSavedTracksCreate track)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = User.GetUserId();
        
        if (await _bll.UserSavedTracksService.ExistsByTrackAndUserAsync(track.TrackId, userId))
        {
            return BadRequest("Track is already saved by this user.");
        }
        
        var bllEntity = _savedTracksMapper.Map(track, userId)!;
        
        _bll.UserSavedTracksService.Add(bllEntity);
        
        await _bll.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetSavedTracks), 
            new { id = bllEntity.Id }, 
            _savedTracksMapper.Map(bllEntity));
    }
    
    /// <summary>
    /// Remove a saved track by the UserSavedTrack id.
    /// </summary>
    /// <param name="id">Id of the UserSavedTrack</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RemoveSavedTrack(Guid id)
    {
        var trackExists = await _bll.UserSavedTracksService.ExistsAsync(id, User.GetUserId());
        if (!trackExists)
        {
            return NotFound();
        }
        
        await _bll.UserSavedTracksService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();

        return NoContent();
    }
    
    /// <summary>
    /// Remove a saved track by the trackid.
    /// </summary>
    /// <param name="trackId">Id of the track to be removed.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{trackId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RemoveByTrackId(Guid trackId)
    {
        var userId = User.GetUserId();
        
        if (!await _bll.UserSavedTracksService.ExistsByTrackAndUserAsync(trackId, userId))
        {
            return NotFound();
        }
        
        await _bll.UserSavedTracksService.RemoveByTrackIdAsync(trackId, userId);
        await _bll.SaveChangesAsync();

        return NoContent();
    }

}