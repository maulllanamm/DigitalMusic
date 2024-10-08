﻿using AutoMapper;
using AutoMapper.EquivalencyExpression;
using DigitalMusic.Application.Features.AlbumFeatures.Command.CreateAlbum;
using DigitalMusic.Application.Features.AlbumFeatures.Command.UpdateAlbum;
using DigitalMusic.Application.Features.AlbumFeatures.Query.GetById;
using DigitalMusic.Application.Features.AuthFeatures.LoginFeatures;
using DigitalMusic.Application.Features.AuthFeatures.RegisterFeatures;
using DigitalMusic.Application.Features.PlaylistFeatures.Command.CreatePlaylist;
using DigitalMusic.Application.Features.PlaylistFeatures.Command.UpdatePlaylist;
using DigitalMusic.Application.Features.PlaylistFeatures.Query.GetAll;
using DigitalMusic.Application.Features.PlaylistFeatures.Query.GetById;
using DigitalMusic.Application.Features.SongFeatures.Command.CreateAlbum;
using DigitalMusic.Application.Features.SongFeatures.Command.UpdateSong;
using DigitalMusic.Application.Features.SongFeatures.Query.GetAll;
using DigitalMusic.Application.Features.SongFeatures.Query.GetById;
using DigitalMusic.Application.Features.UserFeatures.Command.UpdateUser;
using DigitalMusic.Application.Features.UserFeatures.Query.GetAll;
using DigitalMusic.Application.Features.UserFeatures.Query.GetById;
using DigitalMusic.Application.Features.UserFeatures.Query.GetByUsername;
using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application
{
    public class AutoMapperProfilling : Profile
    {
        public AutoMapperProfilling()
        {
            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();

            CreateMap<UpdateUserRequestDTO, UpdateUserRequest>();
            
            CreateMap<UpdateUserRequest, User>();
            CreateMap<User, UpdateUserResponse>();

            CreateMap<GetAllUserRequest, User>();
            CreateMap<User, GetAllUserResponse>();            
            
            CreateMap<GetByIdUserRequest, User>();
            CreateMap<User, GetByIdUserResponse>();            
            
            CreateMap<GetByUsernameRequest, User>();
            CreateMap<User, GetByUsernameResponse>();

            CreateMap<RegisterRequest, User>();
            CreateMap<User, RegisterResponse>();

            CreateMap<LoginRequest, User>();
            CreateMap<User, LoginResponse>();
            
            CreateMap<CreateAlbumRequest, Album>();
            
            CreateMap<Album, GetByIdAlbumResponse>();
            
            CreateMap<Album, GetAllAlbumResponse>();
            
            CreateMap<UpdateAlbumRequest, Album>();
            CreateMap<Album, UpdateAlbumResponse>();
            
            CreateMap<CreateSongRequest, Song>();
            
            CreateMap<Song, GetAllSongResponse>()
                .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.album_id)); 
            
            CreateMap<Song, GetByIdSongResponse>()
                .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.album_id));

            CreateMap<UpdateSongRequest, Song>();
            
            CreateMap<Song, UpdateSongResponse>()
                .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.album_id));
            
            CreateMap<CreatePlaylistRequest, Playlist>();

            CreateMap<Playlist, GetAllPlaylistResponse>();

            CreateMap<Playlist, GetByIdPlaylistResponse>();

            CreateMap<UpdatePlaylistRequest, Playlist>();

            CreateMap<Playlist, UpdatePlaylistResponse>();
        }
    }
}
