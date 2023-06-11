﻿using MusicMarket.Core.Models;

namespace MusicMarket.Api.DTO
{
    public class MusicDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ArtistDTO Artist { get; set; }
    }
}
