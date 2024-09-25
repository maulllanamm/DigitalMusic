using AutoMapper;
using DigitalMusic.Application.Repositories;
using MediatR;

namespace DigitalMusic.Application.Features.PlaylistFeatures.Query.GetAll
{
    public class GetAllPlaylistHandler : IRequestHandler<GetAllPlaylistRequest, List<GetAllPlaylistResponse>>
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IMapper _mapper;
        
        public GetAllPlaylistHandler(IPlaylistRepository playlistRepository, IMapper mapper)
        {
            _playlistRepository = playlistRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPlaylistResponse>> Handle(GetAllPlaylistRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var playlists = await _playlistRepository.GetAll();
                return _mapper.Map<List<GetAllPlaylistResponse>>(playlists);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
