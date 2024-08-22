using AutoMapper;
using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Command.CreateAlbum
{
    public sealed class UpdateAlbumHandler : IRequestHandler<UpdateAlbumRequest, UpdateAlbumResponse>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;
        public UpdateAlbumHandler(IMapper mapper, IAlbumRepository albumRepository)
        {
            _mapper = mapper;
            _albumRepository = albumRepository;
        }

        public async Task<UpdateAlbumResponse> Handle(UpdateAlbumRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var newAlbum = _mapper.Map<Album>(request);
                var album = await _albumRepository.GetById(request.Id);
                album.name = newAlbum.name;
                album.year = newAlbum.year;
                var res = await _albumRepository.Update(album);
                
                return _mapper.Map<UpdateAlbumResponse>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
