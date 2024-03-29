﻿using System.ComponentModel.DataAnnotations;

namespace ConsumingAnimeApi.Models
{
    public class AnimeViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Review { get; set; }
        public float Ratintg { get; set; }
        public string? Streaming { get; set; }
    }
}
