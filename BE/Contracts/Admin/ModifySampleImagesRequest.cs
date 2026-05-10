using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HIPA_BE.Contracts.Admin
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum ModifyAction {
    ToggleHide,
  }
  public record ModifySampleImageRequest([Required] List<int> IDs, [Required] ModifyAction Action);
}