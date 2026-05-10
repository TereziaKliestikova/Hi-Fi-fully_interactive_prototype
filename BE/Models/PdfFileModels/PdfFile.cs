

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIPA_BE.Models.BodySystemModels;
using HIPA_BE.Models.OrganModels;
using Directory = HIPA_BE.Models.LearningModels.Directory;

namespace HIPA_BE.Models.PdfFileModels
{
  public class PdfFile
  {
    [Required]
    [Key]
    public int ID { get; set; }

    [Required]
    [StringLength(120)]
    public required string Name { get; set; }

    [Required]
    public required string Path { get; set; }

    [ForeignKey("BodySystemID")]
    public BodySystem? BodySystem { get; set; }

    [ForeignKey("OrganID")]
    public Organ? Organ { get; set; }

    [ForeignKey("Directory")] 
    public int? DirectoryId { get; set; }

    public Directory? Directory { get; set; }

  }
}