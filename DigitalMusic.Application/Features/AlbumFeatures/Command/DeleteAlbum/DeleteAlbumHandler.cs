using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using DigitalMusic.Domain.Entities;
using MediatR;

namespace DigitalMusic.Application.Features.UserFeatures.Command.CreateAlbum
{
    public sealed class DeleteAlbumHandler : IRequestHandler<DeleteAlbumRequest, bool>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;
        public DeleteAlbumHandler(IMapper mapper, IAlbumRepository albumRepository)
        {
            _mapper = mapper;
            _albumRepository = albumRepository;
        }

        public async Task<bool> Handle(DeleteAlbumRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var album = await _albumRepository.GetById(request.Id);
                if (album == null)
                {
                    throw new NotFoundException("Album Not Found");
                }

                return await _albumRepository.Delete(request.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
