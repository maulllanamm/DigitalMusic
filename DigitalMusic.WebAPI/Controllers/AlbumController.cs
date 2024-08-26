using DigitalMusic.Application.Features.AlbumFeatures.Command.CreateAlbum;
using DigitalMusic.Application.Features.AlbumFeatures.Command.DeleteAlbum;
using DigitalMusic.Application.Features.AlbumFeatures.Command.DeleteImage;
using DigitalMusic.Application.Features.AlbumFeatures.Command.UpdateAlbum;
using DigitalMusic.Application.Features.AlbumFeatures.Command.UploadImage;
using DigitalMusic.Application.Features.AlbumFeatures.Query.GetById;
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
        
        [HttpGet]
        public async Task<ActionResult<List<GetAllAlbumResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var cacheData = _cacheHelper.GetData<IEnumerable<GetAllAlbumResponse>>("albums");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return Ok(cacheData);
            }
            var result = await _mediator.Send(new GetAllAlbumRequest(), cancellationToken);
            var expireTime = DateTime.Now.AddMinutes(1);
            _cacheHelper.SetData<IEnumerable<GetAllAlbumResponse>>("albums", result, expireTime);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
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
        
        [HttpPost("{id}/covers")]
        public async Task<ActionResult<string>> UploadImage(Guid id, [FromForm] UploadImageDTO dto,
            CancellationToken cancellationToken)
        {
            var request = new UploadImageRequest
            (
                id,
                dto.ImageFile
            );
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateAlbumResponse>> Update(Guid id, UpdateAlbumDTO dto,
            CancellationToken cancellationToken)
        {
            var request = new UpdateAlbumRequest(
                id,
                dto.Name,
                dto.Year
            );
            
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteAlbumRequest(id), cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}/covers")]
        public async Task<ActionResult<bool>> DeleteCover(Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteImageRequest(id), cancellationToken);
            return Ok(result);
        }

        
    }
}