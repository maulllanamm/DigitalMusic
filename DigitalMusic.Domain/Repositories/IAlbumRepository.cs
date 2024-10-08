﻿using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Repositories
{
    public interface IAlbumRepository
    {
        public Task<List<Album>> GetAll();
        public Task<Album> GetById(Guid id);
        public Task<Album> Create(Album album);
        public Task<Album> Update(Album album);
        public Task<bool> Delete(Guid id);
    }
}
