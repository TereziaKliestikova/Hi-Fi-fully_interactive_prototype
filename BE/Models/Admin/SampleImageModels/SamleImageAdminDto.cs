using System.ComponentModel.DataAnnotations;
using HIPA_BE.Models.Admin.FlagModels;
using HIPA_BE.Models.PdfFileModels;

namespace HIPA_BE.Models.Admin.SampleImageModels
{
    // This class is used to return a list of images with their diagnoses and annotations
    public class SampleImageAdminDto
    {
        [Required]
        public required int ID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required bool HasAnnotation { get; set; }

        [Required]
        public required bool IsVisible{ get; set; }

        [Required]
        public required string Path { get; set; }

        public FlagTypeDto? FlagType  { get; set; }

        public PdfFile? CaustryFile { get; set; }

        [Required]
        public required string OrganName { get; set; }

        [Required]
        public required List<string> BodySystemNames { get; set; }

        public string? Note { get; set; }
        public string? KeyWords { get; set; }

    }
}