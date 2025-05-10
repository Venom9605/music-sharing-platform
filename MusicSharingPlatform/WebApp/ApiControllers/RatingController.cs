using App.BLL.Interfaces;
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
public class RatingController : ControllerBase
{
    private readonly IAppBLL _bll;
    
    private readonly App.DTO.v1.Mappers.RatingMapper _mapper =
        new App.DTO.v1.Mappers.RatingMapper();

    public RatingController(IAppBLL bll)
    {
        _bll = bll;
    }
    
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
    
    [HttpGet("{trackId}")]
    public async Task<ActionResult<double>> GetAverage(Guid trackId)
    {
        var res = await _bll.RatingService.GetAverageScoreAsync(trackId);

        return Ok(res);
    }
    
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