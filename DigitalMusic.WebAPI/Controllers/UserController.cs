using System.Security.Claims;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Features.UserFeatures.Command.DeleteUser;
using DigitalMusic.Application.Features.UserFeatures.Command.UpdateUser;
using DigitalMusic.Application.Features.UserFeatures.Query.GetAll;
using DigitalMusic.Application.Features.UserFeatures.Query.GetById;
using DigitalMusic.Application.Helper.EnumCollection;
using DigitalMusic.Application.Helper.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMusic.WebAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICacheHelper _cacheHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IMediator mediator, ICacheHelper cacheHelper, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _cacheHelper = cacheHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<GetAllUserResponse>> GetAll(CancellationToken cancellationToken)
        {
            var cacheData = _cacheHelper.GetData<IEnumerable<GetAllUserResponse>>("users");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return Ok(cacheData);
            }
            var result = await _mediator.Send(new GetAllUserRequest(), cancellationToken);
            var expireTime = DateTime.Now.AddMinutes(1);
            _cacheHelper.SetData<IEnumerable<GetAllUserResponse>>("users", result, expireTime);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetByIdUserResponse>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var claimId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != UserRole.Administrator && claimId != id.ToString())
            {
                throw new ForbiddenException( "You haven't permission to get this id.");
            }
            var result = await _mediator.Send(new GetByIdUserRequest(id), cancellationToken);
            return Ok(result);
        }
        
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateUserResponse>> Update(Guid id, UpdateUserRequestDTO dto,
           CancellationToken cancellationToken)
        {
            var request = new UpdateUserRequest(
                id,
                dto.Username,
                dto.Email,
                dto.Fullname,
                dto.PhoneNumber,
                dto.Address
            );
            
            var claimId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != UserRole.Administrator && claimId != id.ToString())
            {
                throw new ForbiddenException( "You haven't permission to update this user.");
            }
            
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteUserRequest>> Delete(Guid id,
           CancellationToken cancellationToken)
        {
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != UserRole.Administrator )
            {
                throw new ForbiddenException( "You haven't permission to delete this user.");
            }
            
            var result = await _mediator.Send(new DeleteUserRequest(id), cancellationToken);
            return Ok(result);
        }
    }
}