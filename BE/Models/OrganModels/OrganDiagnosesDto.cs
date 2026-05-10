using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Models.OrganModels
{
    // Defines a single organ name
    public class OrganDiagnosesDto
    {
        [Required]
        public required int ID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string IconPath { get; set; }
        [Required]
        public required List<Diagnosis> Diagnoses { get; set; }
    }
}