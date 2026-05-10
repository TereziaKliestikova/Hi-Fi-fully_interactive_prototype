using ErrorOr;
using HIPA_BE.Models.SampleImageAnnotationModels;

namespace HIPA_BE.Services.SampleImageAnnotationServices
{
    public interface ISampleImageAnnotationService
    {
        /// <summary>
        /// Get a list of SampleImageAnnotations by the given SampleImageID.
        /// </summary>
        /// <param name="id">Id of required SampleImage.</param>
        /// <returns>
        /// A list of SampleImageAnnotationDto.
        /// <example>
        /// Example of a response:
        /// <code>
        /// {
        ///     "sampleImageAnnotations": [
        ///         {
        ///             "name": "<annot_name>",
        ///             "description": "<annot_description>",
        ///             "boundingBox": "<geojson_boundingBox>"
        ///         }
        ///     ]
        /// }
        /// </code>
        /// </example>
        /// </returns>
        public Task<ErrorOr<SampleImageAnnotationsListDto>> GetSampleImageAnnotationsById(int id);
    }
}