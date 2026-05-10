using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Models.BodySystemModels
{
    public class BodySystemDescriptionDto
    {
        [Required]
        public required int ID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}