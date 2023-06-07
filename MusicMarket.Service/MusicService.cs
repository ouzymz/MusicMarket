using MusicMarket.Core;
using MusicMarket.Core.Models;
using MusicMarket.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMarket.Service
{
    public class MusicService : IMusicService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MusicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Music> CreateMusic(Music newMusic)
        {
            await _unitOfWork.Musics.AddAsync(newMusic);
            await _unitOfWork.CommitAsync();
            return newMusic;
        }

        public async Task DeleteMusic(Music musicToBeDeleted)
        {
            _unitOfWork.Musics.remove(musicToBeDeleted);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Music>> GetAllWithArtist()
        {
            return await _unitOfWork.Musics.GetAllWithArtistAsync();
        }

        public async Task<Music> GetMusicById(int id)
        {
            return await _unitOfWork.Musics.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Music>> GetMusicsByArtistId(int artistId)
        {
            return await _unitOfWork.Musics.GetAllWithArtisyByArtistIdAsync(artistId);
        }

        public async Task UpdateMusic(Music musicToBeUpdated, Music music)
        {
            musicToBeUpdated.Artist = music.Artist;
            musicToBeUpdated.ArtistId = music.Artist.Id;
            musicToBeUpdated.Name= music.Name;

            await _unitOfWork.CommitAsync();
        }
    }
}
