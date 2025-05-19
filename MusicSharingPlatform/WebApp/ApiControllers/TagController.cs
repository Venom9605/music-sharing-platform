using App.BLL.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using App.DTO.v1.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

/// <summary>
/// Tag API controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TagController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly TagMapper _mapper = new TagMapper();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bll"></param>
    public TagController(IAppBLL bll)
    {
        _bll = bll;
    }

    /// <summary>
    /// Retrieve all tags
    /// </summary>
    /// <returns>All tags</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<App.DTO.v1.Tag>>> GetAll()
    {
        var data = await _bll.TagService.AllAsync();
        var result = data.Select(e => _mapper.Map(e)!);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<App.DTO.v1.Tag>> Get(Guid id)
    {
        var tag = await _bll.TagService.FindAsync(id);
        if (tag == null) return NotFound();
        return _mapper.Map(tag)!;
    }
    
    [HttpPost]
    public async Task<ActionResult<App.DTO.v1.Tag>> Create(App.DTO.v1.TagCreate tagDto)
    {
        var tag = _mapper.Map(tagDto)!;
        _bll.TagService.Add(tag);
        await _bll.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new
        {
            id = tag.Id,
            version = HttpContext.GetRequestedApiVersion()!.ToString()
        }, tag);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, App.DTO.v1.Tag tagDto)
    {
        if (id != tagDto.Id) return BadRequest();

        var existing = await _bll.TagService.FindAsync(id);
        if (existing == null) return NotFound();

        var updated = _mapper.Map(tagDto)!;
        _bll.TagService.Update(updated);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existing = await _bll.TagService.FindAsync(id);
        if (existing == null) return NotFound();

        _bll.TagService.Remove(existing);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
    
}