using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIPA_BE.Models.BodySystemModels;
using HIPA_BE.Models.PdfFileModels;

namespace HIPA_BE.Models.Admin.FlagModels
{
    public class FlagTypeDto
    {
        public int ID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Color { get; set; }
    }
}