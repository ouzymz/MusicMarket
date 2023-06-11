using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicMarket.Api.DTO;
using MusicMarket.Api.FluentValidation;
using MusicMarket.Core.Models;
using MusicMarket.Core.Services;
using MusicMarket.Service;

namespace MusicMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistController(IArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }


        [HttpPost("CreateArtist")]
        public async Task<ActionResult<Artist>> CreateArtist(SaveArtistDTO saveArtistResource)
        {
            var validator = new SaveArtistResourceValidator();
            var validationResult = await validator.ValidateAsync(saveArtistResource);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var artistToCreate = _mapper.Map<SaveArtistDTO, Artist>(saveArtistResource);
            var createdArtist = await _artistService.CreateArtist(artistToCreate);
            return Ok(createdArtist);
        }

        [HttpGet("GetAllArtist")]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetAllArtist()
        {
            var artistsModel = await _artistService.GetAllArtist();

            if (artistsModel is not null)
            {
                var artistsDTO = _mapper.Map<IEnumerable<ArtistDTO>>(artistsModel);
                return Ok(artistsDTO);
            }
            else
                return BadRequest();
        }

        [HttpGet("GetArtistById/{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtistById(int id)
        {
            var artist = await _artistService.GetArtistById(id);

            if (artist is not null)
            {
                var artistDTO = _mapper.Map<ArtistDTO>(artist);
                return Ok(artistDTO);
            }
            else
                return BadRequest();
        }

        [HttpPut("UpdateArtist/{id}")]
        public async Task<IActionResult> UpdateArtist(int id, ArtistDTO updatedArtist)
        {
            var updateArtist = _mapper.Map<Artist>(updatedArtist);
            await _artistService.UpdateArtist(id, updateArtist);
            return Ok();


        }

        [HttpDelete("DeleteArtist")]
        public async Task<IActionResult> DeleteArtist(ArtistDTO artistDTO)
        {
            var artistModel = await _artistService.GetArtistById(artistDTO.Id);
            await _artistService.DeleteArtist(artistModel);
            return Ok();
        }
    }
}
