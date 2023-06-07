using Microsoft.EntityFrameworkCore;
using MusicMarket.Core.Models;
using MusicMarket.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMarket.Data.Repositories
{
    public class MusicRepository : Repository<Music>, IMusicRepository
    {
        public MusicRepository(MusicMarketDbContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistAsync()
        {
            return await context.Musics.Include(x=>x.Artist).ToListAsync();
        }

        public async Task<IEnumerable<Music>> GetAllWithArtisyByArtistIdAsync(int artistId)
        {
            return await context.Musics.Include(x=>x.Artist).Where(x=>x.ArtistId== artistId).ToListAsync();
        }

        public async Task<Music> GetWithArtistByIdAsync(int id)
        {
            return await context.Musics.Include(x => x.Artist).SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
