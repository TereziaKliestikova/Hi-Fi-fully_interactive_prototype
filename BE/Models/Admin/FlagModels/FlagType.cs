using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIPA_BE.Models.BodySystemModels;
using HIPA_BE.Models.PdfFileModels;

namespace HIPA_BE.Models.Admin.FlagModels
{
    public class FlagType
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        public required string Name { get; set; }

        [Required]
        [StringLength(60)]
        public required string Color { get; set; }
    }
}