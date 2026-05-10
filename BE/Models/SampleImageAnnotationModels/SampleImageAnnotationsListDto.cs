using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Models.SampleImageAnnotationModels
{
    public class SampleImageAnnotationsListDto
    {
        [Required]
        public required List<SampleImageAnnotationDto> SampleImageAnnotations { get; set; }
    }
}