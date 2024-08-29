using AutoMapper;
using DigitalMusic.Application.Common.Exceptions;
using DigitalMusic.Application.Repositories;
using MediatR;

namespace DigitalMusic.Application.Features.SongFeatures.Command.DeleteSong
{
    public sealed class DeleteSongHandler : IRequestHandler<DeleteSongRequest, bool>
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;
        public DeleteSongHandler(IMapper mapper, ISongRepository songRepository)
        {
            _mapper = mapper;
            _songRepository = songRepository;
        }

        public async Task<bool> Handle(DeleteSongRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var song = await _songRepository.GetById(request.Id);
                if (song == null)
                {
                    throw new NotFoundException("Song Not Found");
                }

                return await _songRepository.Delete(request.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
