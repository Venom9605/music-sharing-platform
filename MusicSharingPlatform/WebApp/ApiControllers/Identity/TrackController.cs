using App.DAL.Interfaces;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using App.DAL.DTO;

namespace WebApp.ApiControllers.Identity;


[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TrackController : ControllerBase
{
    private readonly IAppUOW _uow;

    public TrackController(IAppUOW uow)
    {
        _uow = uow;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
    {
        return (await _uow.TrackRepository.AllAsync(User.GetUserId())).ToList();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Track>> GetTrack(Guid id)
    {
        var track = await _uow.TrackRepository.FindAsync(id, User.GetUserId());
        if (track == null)
        {
            return NotFound();
        }
        return track;
    }
    
    [HttpPost]
    public async Task<ActionResult<Track>> CreateTrack([FromBody] Track dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var track = new Track
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            FilePath = dto.FilePath,
            CoverPath = dto.CoverPath,
            Duration = 200,
            TimesPlayed = 0,
            TimesSaved = 0
        };

        _uow.TrackRepository.Add(track);
        await _uow.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTrack), new { id = track.Id }, track);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrack(Guid id, [FromBody] Track track)
    {
        if (id != track.Id)
        {
            return BadRequest();
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = await _uow.TrackRepository.FindAsync(id, User.GetUserId());
        if (existing == null)
        {
            return NotFound();
        }

        existing.Title = track.Title;
        existing.CoverPath = track.CoverPath;

        _uow.TrackRepository.Update(existing);
        await _uow.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrack(Guid id)
    {
        var track = await _uow.TrackRepository.FindAsync(id, User.GetUserId());
        if (track == null)
        {
            return NotFound();
        }

        await _uow.TrackRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();

        return NoContent();
    }


}