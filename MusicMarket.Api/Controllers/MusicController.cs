using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicMarket.Api.DTO;
using MusicMarket.Api.FluentValidation;
using MusicMarket.Core.Models;
using MusicMarket.Core.Services;
using MusicMarket.Service;
using System;

namespace MusicMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly IMapper _mapper;
        public MusicController(IMusicService musicService, IMapper mapper)
        {
            _musicService = musicService;
            _mapper = mapper;
        }


        [HttpGet("GetAllMusic")]//swagger uzerinde action name gorunmesi icin yapildi.
        public async Task<ActionResult<IEnumerable<MusicDTO>>> GetAllMusic()
        {
            try
            {
                var musics = await _musicService.GetAllWithArtist();
                var mappedMusics = _mapper.Map<IEnumerable<MusicDTO>>(musics);

                return Ok(mappedMusics);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpGet("GetMusicById/{id}")]
        public async Task<ActionResult<MusicDTO>> GetMusicById(int id)
        {
            try
            {

                var music = await _musicService.GetMusicById(id);
                var mappedMusic = _mapper.Map<MusicDTO>(music);

                if (mappedMusic is not null)
                    return Ok(mappedMusic);
                else
                    return BadRequest();

            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpGet("GetMusicsByArtistId/{id}")]
        public async Task<ActionResult<IEnumerable<Music>>> GetMusicsByArtistId(int id)
        {
            var musics = await _musicService.GetMusicsByArtistId(id);
            if (musics is not null)
            {
                return Ok(musics);
            }
            else 
                return BadRequest();
        }

        [HttpPost("CreateMusic")]
        public async Task<ActionResult<Music>> CreateMusic(SaveMusicDTO saveMusicResource)
        {
            var validator = new SaveMusicResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMusicResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var musicToCreate = _mapper.Map<SaveMusicDTO, Music>(saveMusicResource);
            var createdMusic = await _musicService.CreateMusic(musicToCreate);
            return Ok(createdMusic);

        }


        [HttpDelete("DeleteMusic/{Id}")]
        public async Task<IActionResult> DeleteMusic(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }

            var music = await _musicService.GetMusicById(Id);

            if (music != null)
            {
                _musicService.DeleteMusic(music);
                return Ok();
            }

            else
                return NotFound();
        }

        [HttpPut("UpdateMusic")]
        public async Task<ActionResult<MusicDTO>> UpdateMusic(int id, SaveMusicDTO sourceMusic)
        {
            var validator = new SaveMusicResourceValidator();
            var validationResult = await validator.ValidateAsync(sourceMusic);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);



            var musicToUpdate = await _musicService.GetMusicById(id);

            if (musicToUpdate == null)
                return NotFound();

            var newMusic = _mapper.Map<SaveMusicDTO, Music>(sourceMusic);

            await _musicService.UpdateMusic(musicToUpdate, newMusic);

            var music = await _musicService.GetMusicById(id);

            var musicDTO = _mapper.Map<MusicDTO>(music);
            return Ok(musicDTO);
        }
    }
}
