namespace HIPA_BE.Models.PdfFileModels
{
  public class PdfFileDto
  {
    public int ID { get; set; }
    public required string Name { get; set; }
    public required string Path { get; set; }
  }
}