using DigitalMusic.Application.Features.SongFeatures.Command.CreateAlbum;
using DigitalMusic.Application.Features.SongFeatures.Command.DeleteSong;
using DigitalMusic.Application.Features.SongFeatures.Command.UpdateSong;
using DigitalMusic.Application.Features.SongFeatures.Query.GetAll;
using DigitalMusic.Application.Features.SongFeatures.Query.GetById;
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

    [HttpGet]
    public async Task<ActionResult<List<GetAllSongResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var cacheData = _cacheHelper.GetData<IEnumerable<GetAllSongResponse>>("songs");
        if (cacheData != null && cacheData.Count() > 0)
        {
            return Ok(cacheData);
        }

        var result = await _mediator.Send(new GetAllSongRequest(), cancellationToken);
        var expireTime = DateTime.Now.AddMinutes(1);
        _cacheHelper.SetData<IEnumerable<GetAllSongResponse>>("Songs", result, expireTime);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdSongResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetByIdSongRequest(id), cancellationToken);
        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateSongRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateSongResponse>> Update(Guid id, UpdateSongDTO dto,
        CancellationToken cancellationToken)
    {
        var request = new UpdateSongRequest(
            id,
            dto.Title,
            dto.Year,
            dto.Performer,
            dto.Genre,
            dto.Duration,
            dto.AlbumId
        );

        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteSongRequest(id), cancellationToken);
        return Ok(result);
    }
}