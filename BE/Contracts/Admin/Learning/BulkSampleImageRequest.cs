using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Contracts.Admin.Learning;

public record BulkSampleImageRequest([Required] List<int> SampleImageIds);