using App.DAL.Interfaces;
using Base.Helpers;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers.Identity;


[Route("api/[controller]")]
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

}