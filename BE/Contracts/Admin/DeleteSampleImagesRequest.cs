using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Contracts.Admin
{
  public record DeleteSampleImageRequest([Required] List<int> IDs);
}