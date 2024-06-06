using System.ComponentModel.DataAnnotations;

namespace Bhandari.API.Models.DTOs
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum length of 3 character")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum length of 3 character")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum length of 100 character")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
