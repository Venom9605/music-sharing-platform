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
public class PlaylistController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly App.DTO.v1.Mappers.PlaylistMapper _mapper = new App.DTO.v1.Mappers.PlaylistMapper();

    public PlaylistController(IAppBLL bll)
    {
        _bll = bll;
    }
    
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<App.DTO.v1.Playlist>), 200)]
    public async Task<ActionResult<IEnumerable<App.DTO.v1.Playlist>>> GetAllPlaylists()
    {
        var userId = User.GetUserId();

        var data = await _bll.PlaylistService.AllAsync(userId);
        var res = data.Select(p => _mapper.Map(p)!).ToList();

        return Ok(res);
    }
    
}