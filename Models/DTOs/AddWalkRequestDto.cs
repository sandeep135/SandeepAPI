﻿using Bhandari.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Bhandari.API.Models.DTOs
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        [Range(0,50)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }

       
    }
}
