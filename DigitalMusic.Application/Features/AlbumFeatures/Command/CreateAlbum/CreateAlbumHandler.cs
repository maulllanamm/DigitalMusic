using AutoMapper;
using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Features.AlbumFeatures.Command.CreateAlbum
{
    public sealed class CreateAlbumHandler : IRequestHandler<CreateAlbumRequest, Guid>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateAlbumHandler(IMapper mapper, IAlbumRepository albumRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _albumRepository = albumRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> Handle(CreateAlbumRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var album = _mapper.Map<Album>(request);
                var res = await _albumRepository.Create(album);
                return res.id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
