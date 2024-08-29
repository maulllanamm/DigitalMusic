using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using MediatR;

namespace DigitalMusic.Application.Features.SongFeatures.Query.GetById
{
    public class GetByIdSongHandler : IRequestHandler<GetByIdSongRequest, GetByIdSongResponse>
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;
        
        public GetByIdSongHandler(ISongRepository songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdSongResponse> Handle(GetByIdSongRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var song = await _songRepository.GetById(request.id);
                if (song == null)
                {
                    throw new NotFoundException("Song Not Found");
                }
                return _mapper.Map<GetByIdSongResponse>(song);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
