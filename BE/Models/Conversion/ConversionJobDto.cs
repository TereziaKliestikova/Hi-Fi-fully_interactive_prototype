using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Models.SampleImageModels
{
    public class ConversionJobDto
    {
        [Required]
        public required int SampleImageID { get; set; }

        public string? SampleImageName { get; set; } = null;

        public string? SampleImageGroupId { get; set; } = null;

        public string? SampleImagePath { get; set; } = null;

        public string? State { get; set; } = null;

        public SampleImageMetadataDto? Metadata { get; set; } = null;
    }
}
