using System.ComponentModel.DataAnnotations;
using HIPA_BE.Models.OrganModels;
using HIPA_BE.Models.PdfFileModels;

namespace HIPA_BE.Models.BodySystemModels
{
    public class BodySystem
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        [StringLength(256)]
        public required string IconPath { get; set; }

        public List<Organ> Organs { get; set; }
        public List<PdfFile> Pdfs { get; set; }

    }
}