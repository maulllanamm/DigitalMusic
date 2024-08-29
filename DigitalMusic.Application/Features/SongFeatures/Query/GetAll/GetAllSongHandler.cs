using AutoMapper;
using DigitalMusic.Application.Repositories;
using MediatR;

namespace DigitalMusic.Application.Features.SongFeatures.Query.GetAll
{
    public class GetAllSongHandler : IRequestHandler<GetAllSongRequest, List<GetAllSongResponse>>
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;
        
        public GetAllSongHandler(ISongRepository songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllSongResponse>> Handle(GetAllSongRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var songs = await _songRepository.GetAll();
                return _mapper.Map<List<GetAllSongResponse>>(songs);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
