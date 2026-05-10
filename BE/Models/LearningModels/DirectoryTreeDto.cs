using HIPA_BE.Models.PdfFileModels;

namespace HIPA_BE.Models.LearningModels;

public class DirectoryTreeDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required bool IsPublic { get; set; }
    // public string? Description { get; set; }
    // public string? KeyWords { get; set; }

    // public List<PdfFile> Files { get; set; }

    public List<DirectoryTreeDto> Children { get; set; }
}