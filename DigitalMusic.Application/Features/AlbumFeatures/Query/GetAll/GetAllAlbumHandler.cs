using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Query.GetById
{
    public class GetAllAlbumHandler : IRequestHandler<GetAllAlbumRequest, List<GetAllAlbumResponse>>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;
        
        public GetAllAlbumHandler(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllAlbumResponse>> Handle(GetAllAlbumRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var albums = await _albumRepository.GetAll();
                return _mapper.Map<List<GetAllAlbumResponse>>(albums);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
