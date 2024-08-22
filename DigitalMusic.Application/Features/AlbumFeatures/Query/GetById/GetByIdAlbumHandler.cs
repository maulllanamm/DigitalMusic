using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Query.GetById
{
    public class GetByIdAlbumHandler : IRequestHandler<GetByIdAlbumRequest, GetByIdAlbumResponse>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;
        
        public GetByIdAlbumHandler(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdAlbumResponse> Handle(GetByIdAlbumRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var album = await _albumRepository.GetById(request.id);
                if (album == null)
                {
                    throw new NotFoundException("Album Not Found");
                }
                return _mapper.Map<GetByIdAlbumResponse>(album);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
