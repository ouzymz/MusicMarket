﻿using MusicMarket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMarket.Core.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetAllArtist();
        Task<Artist> GetArtistById(int id);
        Task<Artist> CreateArtist(Artist artist);
        Task UpdateArtist(Artist artistToBeUpdated, Artist artist);
        Task DeleteArtist(Artist artistToBeDeleted);
    }
}
