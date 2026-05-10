using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Contracts.Admin.Learning;

public record NewRootDirectoryRequest([Required] string Name,[Required] string StudyCategory);