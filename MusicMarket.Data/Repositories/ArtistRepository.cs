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
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {

        public ArtistRepository(MusicMarketDbContext context):base(context)
        {

        }

        public async Task<IEnumerable<Artist>> GetAllWithMusicAsync()
        {
            return await context.Artist.Include(x=>x.Musics).ToListAsync();
        }

        public async Task<Artist> GetWithMusicByIdAsync(int id)
        {
            return await context.Artist.Include(x=>x.Musics).SingleOrDefaultAsync(x=>x.Id==id);
        }


    }


}
