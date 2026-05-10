using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HIPA_BE.Models.PdfFileModels;

namespace HIPA_BE.Models.LearningModels;

public class Directory
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public StudyCategory StudyCategory { get; set; }
    [Required]
    public string Path { get; set; }
    [Required]
    public int NestingLevel { get; set; }
    [Required]
    public bool IsPublic { get; set; }

    public string? Description { get; set; }

    public string? KeyWords { get; set; }

    [ForeignKey("Parent")]
    public int? ParentId { get; set; }
    public Directory? Parent { get; set; }
    
    public List<Directory> ChildDirectories { get; set; }

    public List<PdfFile> Files { get; set; }

    public List<SampleImage> SampleImages { get; set; }
}