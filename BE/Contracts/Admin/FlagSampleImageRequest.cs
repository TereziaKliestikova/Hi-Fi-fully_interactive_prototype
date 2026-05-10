using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Contracts.Admin
{
  public record FlagSampleImageRequest([Required] int FlagTypeId);
}