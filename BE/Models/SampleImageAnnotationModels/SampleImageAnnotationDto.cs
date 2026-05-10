using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Models.SampleImageAnnotationModels
{
    public class SampleImageAnnotationDto
    {
        [Required]
        public required int ID { get; set; }
        
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }
        
        // column BoundingBox as geo-json
        [Required]
        public required string BoundingBox { get; set; }
    }
}