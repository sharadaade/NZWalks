using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MaxLength(3, ErrorMessage = "Region code must be 3 characters long")]
        [MinLength(3, ErrorMessage = "Region code must be 3 characters long")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Region name must not exceed 100 characters")]
        public string Name { get; set; }


        public string? RegionImageUrl { get; set; }
    }
}
