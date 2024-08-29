using DigitalMusic.Application.Features.SongFeatures.Command.CreateAlbum;
using DigitalMusic.Application.Helper.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMusic.WebAPI.Controllers;


[ApiController]
[Route("songs")]
public class SongController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICacheHelper _cacheHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SongController(IMediator mediator, ICacheHelper cacheHelper, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _cacheHelper = cacheHelper;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateSongRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
}