using System.ComponentModel.DataAnnotations;
using HIPA_BE.Models.PdfFileModels;

namespace HIPA_BE.Models.SampleImageModels
{
    public class SampleImageDto
    {
        [Required]
        public required int ID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Path { get; set; }
        public required bool HasAnnotation { get; set; }
        [Required]
        public required string Diagnosis { get; set; }
        [Required]
        public required bool IsFavorite { get; set; }
        public PdfFile? CaustryFile { get; set; }
        public string? Note { get; set; }
        public string? KeyWords { get; set; }
    }
}

