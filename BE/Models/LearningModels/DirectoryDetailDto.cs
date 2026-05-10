using HIPA_BE.Models.PdfFileModels;

namespace HIPA_BE.Models.LearningModels;
public class DirectoryDetailDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required bool IsPublic { get; set; }
    public required int Level { get; set; }
    public string? Description { get; set; }
    public string? KeyWords { get; set; }
    public List<PdfFileDto> Files { get; set; }
    public List<DirectoryListItemDto> Children { get; set; }
}