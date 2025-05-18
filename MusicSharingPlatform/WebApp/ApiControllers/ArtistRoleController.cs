using App.BLL.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using App.DTO.v1.Mappers;

namespace WebApp.ApiControllers;

/// <summary>
/// Artist Role API controller.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class ArtistRoleController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly ArtistRoleMapper _mapper = new ArtistRoleMapper();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="bll"></param>
    public ArtistRoleController(IAppBLL bll)
    {
        _bll = bll;
    }

    /// <summary>
    /// Retrieves all artist roles.
    /// </summary>
    /// <returns>All artist roles.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App.DTO.v1.ArtistRole>>> GetAll()
    {
        var data = await _bll.ArtistRoleService.AllAsync();
        var result = data.Select(e => _mapper.Map(e)!);
        return Ok(result);
    }
}