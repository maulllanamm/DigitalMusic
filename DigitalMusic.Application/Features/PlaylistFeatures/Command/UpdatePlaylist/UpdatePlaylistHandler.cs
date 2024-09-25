using System.Security.Claims;
using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Command.UpdatePlaylist
{
    public sealed class UpdatePlaylistHandler : IRequestHandler<UpdatePlaylistRequest, UpdatePlaylistResponse>
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public UpdatePlaylistHandler(IPlaylistRepository playlistRepository, IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _playlistRepository = playlistRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdatePlaylistResponse> Handle(UpdatePlaylistRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;
                var newPlaylist = _mapper.Map<Playlist>(request);
                var playlist = await _playlistRepository.GetById(request.Id);
                if (playlist is null)
                {
                    throw new NotFoundException("Playlist doesn't exist");
                }
                if (playlist.user_id != Guid.Parse(userId))
                {
                    throw new ForbiddenException("You don't have permission to update this playlist");
                }
                playlist.name = newPlaylist.name;
                var res = await _playlistRepository.Update(playlist);
                
                return _mapper.Map<UpdatePlaylistResponse>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
