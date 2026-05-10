using ErrorOr;
using HIPA_BE.Models.SampleImageModels;

using HIPA_BE.Models;
using HIPA_BE.Models.Admin.SampleImageModels;

namespace HIPA_BE.Services.SampleImageServices
{
    public interface ISampleImageService
    {
        /// <summary>
        /// Retrieves a sample image data by it's id. Is used when
        /// sample image is requested by the client. This method return the path to the
        /// image so the image handling library on the FE can handle image scaling.
        /// </summary>
        /// <param name="sampleId"></param>
        /// <returns>
        /// <code>
        /// {
        ///
        ///     "ID": "<sample_id>",
        ///     "Name": "<sample_name>",
        ///     "Path": "<sample_path>"
        /// }
        /// </code>
        /// </returns>
        /// 
        Task<ErrorOr<SampleImageDto>> GetSampleImageSampleId(int sampleId,string userId);
        Task<ErrorOr<List<SampleImageAdminDto>>> GetAllSampleImagesForAdmin(string userId);
        Task<ErrorOr<List<SampleImageDiagnosisDto>>> GetAllSampleImages(string userId);
        Task<ErrorOr<List<string>>> GetUniqueSampleImagesKeywords(int organId);
        Task<ErrorOr<Success>> PostFavouiteSampleImageSampleId(string userId, int sampleId);
        Task<ErrorOr<Success>> DeleteFavouiteSampleImageSampleId(string userId, int sampleId);
        Task<ErrorOr<Success>> PostSampleImageNote(string userId, int sampleId, string note);
        Task<ErrorOr<Success>> FlagSampleImage(int sampleId, int flagTypeId, string userId);
        Task<ErrorOr<Success>> UnFlagSampleImage(int sampleId,string userId);
        Task<ErrorOr<SampleImageMetadataDto>> GetSampleImageMetadataById(int sampleId);

    }
}