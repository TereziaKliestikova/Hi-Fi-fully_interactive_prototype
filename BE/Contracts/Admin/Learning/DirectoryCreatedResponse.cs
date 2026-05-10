using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Contracts.Admin.Learning;

public record DirectoryCreatedResponse([Required] int DirectoryId,[Required] string DirectoryName);