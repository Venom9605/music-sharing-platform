﻿using App.BLL.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using App.DTO.v1.Mappers;

namespace WebApp.ApiControllers;

/// <summary>
/// Mood API controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class MoodController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly MoodMapper _mapper = new MoodMapper();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bll"></param>
    public MoodController(IAppBLL bll)
    {
        _bll = bll;
    }

    /// <summary>
    /// Returns all moods
    /// </summary>
    /// <returns>All moods</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App.DTO.v1.Mood>>> GetAll()
    {
        var data = await _bll.MoodService.AllAsync();
        var result = data.Select(e => _mapper.Map(e)!);
        return Ok(result);
    }
}