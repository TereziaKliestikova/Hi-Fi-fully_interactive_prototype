using System.ComponentModel.DataAnnotations;
using HIPA_BE.Models.SampleImageModels;

namespace HIPA_BE.Models.BodySystemModels
{
    // Detail of a single organ with its description and a list of all sample images associated with it
    public class BodySystemDetailDto
    {
        [Required]
        public required BodySystemDescriptionDto BodySystemDescription { get; set; }
        [Required]
        public required List<SampleImageDiagnosisDto> SampleImages { get; set; }
    }
}