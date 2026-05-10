using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Models.BodySystemModels
{
    public class BodySystemListDto
    {
        [Required]
        public required List<BodySystemDto> BodySystems { get; set; }
    }
}