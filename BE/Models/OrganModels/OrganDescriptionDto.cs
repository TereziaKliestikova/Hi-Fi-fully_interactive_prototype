using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Models.OrganModels
{
    public class OrganDescriptionDto
    {
        [Required]
        public required int ID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}