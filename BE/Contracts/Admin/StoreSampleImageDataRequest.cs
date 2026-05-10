using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Contracts.Admin
{
    public record StoreSampleImageDataRequest(
        [Required(ErrorMessage = "api.error.storeSampleImageData.invalidForm")]
        IFormFile AnnotationFile,

        [Required(ErrorMessage = "api.error.storeSampleImageData.invalidForm")]
        string SampleImageFileName,

        [Required(ErrorMessage = "api.error.storeSampleImageData.invalidForm")]
        int OrganId,

        string KeyWords

        // int BodySystemId,

        // string NewBodySystemName,

        // string NewOrganName
        );
}