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
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ArtistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Artist> CreateArtist(Artist artist)
        {
            await _unitOfWork.Artists.AddAsync(artist);//savechanges islemler yapildiktan sonra controller uzerinde cagirilir.
            await _unitOfWork.CommitAsync();
            return artist;
        }

        public async Task DeleteArtist(Artist artistToBeDeleted)
        {
            _unitOfWork.Artists.remove(artistToBeDeleted);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Artist>> GetAllArtist()
        {
            return await _unitOfWork.Artists.GetAllAsync();
        }

        public async Task<Artist> GetArtistById(int id)
        {
            return await _unitOfWork.Artists.GetByIdAsync(id);
        }

        public async Task UpdateArtist(int id, Artist artist)
        {
            var artistToBeUpdated = await _unitOfWork.Artists.GetByIdAsync(id);
            artistToBeUpdated.Name= artist.Name; //change tracker entity'i takip eder eve savechanges ile bu entity üzerindeki degisiklikler direkt update olacak.
            await _unitOfWork.CommitAsync();
        }
    }
}
