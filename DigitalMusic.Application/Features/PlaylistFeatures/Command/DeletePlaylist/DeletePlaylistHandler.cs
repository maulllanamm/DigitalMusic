using System.Security.Claims;
using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Command.DeletePlaylist
{
    public sealed class DeletePlaylistHandler : IRequestHandler<DeletePlaylistRequest, bool>
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeletePlaylistHandler(IMapper mapper, IPlaylistRepository playlistRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _playlistRepository = playlistRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeletePlaylistRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;
                var playlist = await _playlistRepository.GetById(request.Id);
                if (playlist == null)
                {
                    throw new NotFoundException("Playlist Not Found");
                }

                if (playlist.user_id != Guid.Parse(userId))
                {
                    throw new ForbiddenException("You don't have permission to delete this playlist");
                }
                
                return await _playlistRepository.Delete(request.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
