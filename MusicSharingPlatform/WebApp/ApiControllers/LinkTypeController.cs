using App.BLL.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using App.DTO.v1.Mappers;

namespace WebApp.ApiControllers;

/// <summary>
/// Link Type API controller.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class LinkTypeController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly LinkTypeMapper _mapper = new LinkTypeMapper();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="bll"></param>
    public LinkTypeController(IAppBLL bll)
    {
        _bll = bll;
    }

    /// <summary>
    /// Retrieves all link types.
    /// </summary>
    /// <returns>All link types.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App.DTO.v1.LinkType>>> GetAll()
    {
        var data = await _bll.LinkTypeService.AllAsync();
        var result = data.Select(e => _mapper.Map(e)!);
        return Ok(result);
    }
}