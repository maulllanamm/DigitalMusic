using System.Security.Claims;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Features.UserFeatures.Command.CreateAlbum;
using DigitalMusic.Application.Features.UserFeatures.Query.GetById;
using DigitalMusic.Application.Helper.EnumCollection;
using DigitalMusic.Application.Helper.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMusic.WebAPI.Controllers
{
    [ApiController]
    [Route("albums")]
    public class AlbumController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICacheHelper _cacheHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AlbumController(IMediator mediator, ICacheHelper cacheHelper, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _cacheHelper = cacheHelper;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [HttpGet("id")]
        public async Task<ActionResult<GetByIdAlbumResponse>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdAlbumRequest(id), cancellationToken);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateAlbumRequest request,
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

    }
}