using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicMarket.Core.Models;
using MusicMarket.Core.Services;
using MusicMarket.Service;

namespace MusicMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;
        public MusicController(IMusicService musicService)
        {
            _musicService = musicService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Music>>> GetAllMusic()
        {
            var musics =  await _musicService.GetAllWithArtist();
            return Ok(musics);
        }


    }
}
