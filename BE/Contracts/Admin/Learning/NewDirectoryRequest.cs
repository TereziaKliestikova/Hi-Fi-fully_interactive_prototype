using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Contracts.Admin.Learning;

public record NewDirectoryRequest([Required] string Name);