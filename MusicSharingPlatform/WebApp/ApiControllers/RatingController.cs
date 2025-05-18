using App.BLL.Interfaces;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;


/// <summary>
/// Rating/feedback API controller.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class RatingController : ControllerBase
{
    private readonly IAppBLL _bll;
    
    private readonly App.DTO.v1.Mappers.RatingMapper _mapper =
        new App.DTO.v1.Mappers.RatingMapper();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bll"></param>
    public RatingController(IAppBLL bll)
    {
        _bll = bll;
    }

    /// <summary>
    /// Creates a new rating entity for a specific track.
    /// </summary>
    /// <param name="dto">The DTO containing the rating information.</param>
    /// <returns>A response indicating the success or failure of the creation operation.</returns>
    [HttpPost]
    public async Task<IActionResult> Create(App.DTO.v1.RatingCreate dto)
    {
        var userId = User.GetUserId();


        if (await _bll.RatingService.ExistsByTrackAndUserAsync(dto.TrackId, userId))
        {
            return BadRequest("You already rated this track.");
        }

        var bllEntity = _mapper.Map(dto, userId)!;
        _bll.RatingService.Add(bllEntity);
        await _bll.SaveChangesAsync();

        return Ok(_mapper.Map(bllEntity));
    }

    /// <summary>
    /// Updates an existing rating.
    /// </summary>
    /// <param name="id">The ID of the rating to be updated.</param>
    /// <param name="dto">The updated rating DTO containing the new values.</param>
    /// <returns>No content</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(Guid id, App.DTO.v1.RatingEdit dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        var rating = await _bll.RatingService.FindAsync(id, User.GetUserId());
        
        if (rating == null)
        {
            return NotFound();
        }

        rating.Score = dto.Score;
        rating.Comment = dto.Comment;

        _bll.RatingService.Update(rating);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
    
    /// <summary>
    /// Gets the average rating for a specific track.
    /// </summary>
    /// <param name="trackId"></param>
    /// <returns>Average rating double</returns>
    [HttpGet("{trackId}")]
    public async Task<ActionResult<double>> GetAverage(Guid trackId)
    {
        var res = await _bll.RatingService.GetAverageScoreAsync(trackId);

        return Ok(res);
    }
    
    /// <summary>
    /// Delete a rating by its ID.
    /// </summary>
    /// <param name="id">id of the rating to delete</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var rating = await _bll.RatingService.FindAsync(id, User.GetUserId());
        
        if (rating == null)
        {
            return NotFound();
        }

        _bll.RatingService.Remove(rating, User.GetUserId());
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}