using DigitalMusic.Application.Features.PlaylistFeatures.Command.CreatePlaylist;
using DigitalMusic.Application.Features.PlaylistFeatures.Command.DeletePlaylist;
using DigitalMusic.Application.Features.PlaylistFeatures.Command.UpdatePlaylist;
using DigitalMusic.Application.Features.PlaylistFeatures.Query.GetAll;
using DigitalMusic.Application.Features.PlaylistFeatures.Query.GetById;
using DigitalMusic.Application.Helper.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMusic.WebAPI.Controllers;

[ApiController]
[Route("playlists")]
public class PlaylistController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICacheHelper _cacheHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PlaylistController(IMediator mediator, ICacheHelper cacheHelper, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _cacheHelper = cacheHelper;
        _httpContextAccessor = httpContextAccessor;
    }

    [Authorize(policy:"AdminOnly")]
    [HttpGet]
    public async Task<ActionResult<List<GetAllPlaylistResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var cacheData = _cacheHelper.GetData<IEnumerable<GetAllPlaylistResponse>>("playlists");
        if (cacheData != null && cacheData.Count() > 0)
        {
            return Ok(cacheData);
        }

        var result = await _mediator.Send(new GetAllPlaylistRequest(), cancellationToken);
        var expireTime = DateTime.Now.AddMinutes(1);
        _cacheHelper.SetData<IEnumerable<GetAllPlaylistResponse>>("Playlists", result, expireTime);
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdPlaylistResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetByIdPlaylistRequest(id), cancellationToken);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreatePlaylistRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<UpdatePlaylistResponse>> Update(Guid id, UpdatePlaylistDTO dto,
        CancellationToken cancellationToken)
    {
        var request = new UpdatePlaylistRequest(
            id,
            dto.Name
        );

        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeletePlaylistRequest(id), cancellationToken);
        return Ok(result);
    }
}