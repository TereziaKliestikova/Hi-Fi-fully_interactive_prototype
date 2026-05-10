using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIPA_BE.Models.OrganModels;
using HIPA_BE.Models.PdfFileModels;
using Directory = HIPA_BE.Models.LearningModels.Directory;

namespace HIPA_BE.Models
{
    public class SampleImage
    {
        [Key]
        public int ID { get; set; }

        public bool IsVisible { get; set; } = false;

        [StringLength(60)]
        public string? Name { get; set; }

        [StringLength(256)]
        public string? Path { get; set; }

        [ForeignKey("Organ")]
        public int? OrganID { get; set; }
        public Organ? Organ { get; set; } 

        // For Kazuistika column
        [ForeignKey("CaustryFile")]
        public int? CaustryFileID { get; set; }
        public PdfFile? CaustryFile { get; set; }

        public string? KeyWords { get; set; }

        [ForeignKey("Diagnosis")]
        public int? DiagnosisID { get; set; }
        public Diagnosis? Diagnosis { get; set; }

        [Required]
        public DateTime LastModified { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string GroupId {get;set;}

        public List<Directory> ParentDirectories { get; set; }

        public string? Metadata { get; set; }
    }
}
