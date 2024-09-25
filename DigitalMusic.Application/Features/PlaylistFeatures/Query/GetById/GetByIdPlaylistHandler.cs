using System.Security.Claims;
using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Query.GetById
{
    public class GetByIdPlaylistHandler : IRequestHandler<GetByIdPlaylistRequest, GetByIdPlaylistResponse>
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public GetByIdPlaylistHandler( IMapper mapper, IPlaylistRepository playlistRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _playlistRepository = playlistRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetByIdPlaylistResponse> Handle(GetByIdPlaylistRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;
                var playlist = await _playlistRepository.GetById(request.id);
                if (playlist == null)
                {
                    throw new NotFoundException("Playlist Not Found");
                }
                if (playlist.user_id != Guid.Parse(userId))
                {
                    throw new ForbiddenException("You don't have permission to get this playlist");
                }
                return _mapper.Map<GetByIdPlaylistResponse>(playlist);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
