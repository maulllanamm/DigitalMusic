using System.Security.Claims;
using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Command.CreatePlaylist
{
    public sealed class CreatePlaylistHandler : IRequestHandler<CreatePlaylistRequest, Guid>
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreatePlaylistHandler(IMapper mapper, IPlaylistRepository playlistRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _mapper = mapper;
            _playlistRepository = playlistRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreatePlaylistRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var playlist = _mapper.Map<Playlist>(request);
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;
                playlist.user_id = Guid.Parse(userId);
                var res = await _playlistRepository.Create(playlist);
                return res.id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
