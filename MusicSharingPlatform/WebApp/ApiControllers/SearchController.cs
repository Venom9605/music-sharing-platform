using App.BLL.Interfaces;
using App.DTO.v1;
using App.DTO.v1.Mappers;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SearchController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly ArtistMapper _artistMapper = new();
    private readonly TrackMapper _trackMapper = new();

    public SearchController(IAppBLL bll)
    {
        _bll = bll;
    }
    
    [HttpGet("{query}")]
    [ProducesResponseType(typeof(SearchResultsDto), 200)]
    [AllowAnonymous]
    public async Task<ActionResult<SearchResultsDto>> SearchAll(string query)
    {
        var trackResults = await _bll.TrackService.SearchTracksAsync(query);
        var artistResults = await _bll.ArtistService.SearchArtistsAsync(query);

        var dto = new SearchResultsDto
        {
            Tracks = trackResults.Select(t => _trackMapper.Map(t)!).ToList(),
            Artists = artistResults.Select(a => _artistMapper.Map(a)!).ToList()
        };

        return Ok(dto);
    }
}