using System.ComponentModel.DataAnnotations;
using HIPA_BE.Models.PdfFileModels;
using HIPA_BE.Models.SampleImageModels;

namespace HIPA_BE.Models.OrganModels
{
    // Detail of a single organ with its description and a list of all sample images associated with it
    public class OrganDetailDto
    {
        [Required]
        public required OrganDescriptionDto OrganDescription { get; set; }

        public PdfFile? OrganPdf { get; set; }

        [Required]
        public required List<SampleImageDiagnosisDto> SampleImages { get; set; }
    }
}