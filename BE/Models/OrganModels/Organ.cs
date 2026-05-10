using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIPA_BE.Models.BodySystemModels;
using HIPA_BE.Models.PdfFileModels;

namespace HIPA_BE.Models.OrganModels
{
    public class Organ
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        public required string Name { get; set; }

        [Required]
        [StringLength(256)]
        public required string Description { get; set; }

        [Required]
        [StringLength(256)]
        public string IconPath { get; set; }

        [Required]
        public List<BodySystem> BodySystems { get; set; }

        public List<PdfFile> Pdfs { get; set; }
    }
}