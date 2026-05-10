using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Models.OrganModels
{
    // Defines a whole list of organ names
    public class OrgansListDto
    {
        [Required]
        public required List<OrganDiagnosesDto> Organs { get; set; }
    }
}