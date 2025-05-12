using App.BLL.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using App.DTO.v1.Mappers;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class LinkTypeController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly LinkTypeMapper _mapper = new LinkTypeMapper();

    public LinkTypeController(IAppBLL bll)
    {
        _bll = bll;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<App.DTO.v1.LinkType>>> GetAll()
    {
        var data = await _bll.LinkTypeService.AllAsync();
        var result = data.Select(e => _mapper.Map(e)!);
        return Ok(result);
    }
}