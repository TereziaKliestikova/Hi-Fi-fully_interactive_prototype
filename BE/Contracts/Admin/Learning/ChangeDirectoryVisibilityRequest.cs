using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Contracts.Admin.Learning;

public record ChangeDirectoryVisibilityRequest([Required] bool IsPublic);