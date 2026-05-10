using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Models.BaseModels
{
    public class IconPathDto
    {
        [Required]
        public required int ID { get; set; }
        [Required]
        public required string IconPath { get; set; }
    }
}