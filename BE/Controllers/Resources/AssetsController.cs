using HIPA_BE.Models.OrganModels;
using HIPA_BE.Models.DiagnosisModels;
using HIPA_BE.Models.SampleImageAnnotationModels;
using HIPA_BE.Models.SampleImageModels;
using HIPA_BE.Services.DiagnosisServices;
using HIPA_BE.Services.OrganServices;
using HIPA_BE.Services.SampleImageAnnotationServices;
using HIPA_BE.Services.SampleImageServices;
using HIPA_BE.Services;
using HIPA_BE.Models.BodySystemModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using log4net;
using System.Reflection;
using HIPA_BE.Services.BodySystemService;
using HIPA_BE.Models.BaseModels;
using System.Security.Claims;
using HIPA_BE.Contracts;


namespace HIPA_BE.Controllers.Resources
{
    [ApiController]
    [Route("assets")]
    public class AssetsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(AssetsController));

        private readonly BodySystemService _bodySystemService;
        private readonly IOrganService _organService;
        private readonly IDiagnosisService _diagnosisService;
        private readonly ISampleImageService _sampleImageService;
        private readonly ISampleImageAnnotationService _sampleImageAnnotationService;



        public AssetsController(BodySystemService bodySystemService, OrganService organService, DiagnosisService diagnosisService,
            SampleImageService sampleImageService, SampleImageAnnotationService sampleImageAnnotationService)
        {
            _bodySystemService = bodySystemService;
            _organService = organService;
            _diagnosisService = diagnosisService;
            _sampleImageService = sampleImageService;
            _sampleImageAnnotationService = sampleImageAnnotationService;
        }

        [HttpGet("samples-images-all")]
        [Authorize]
        [ProducesResponseType(typeof(List<SampleImageDiagnosisDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSampleImages()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            if (String.IsNullOrEmpty(userId))
            {
                Log.Warn("Returning BadRequest. User is not authenticated or failed to parse identity.");
                return BadRequest();
            }

            var samplesResult = await _sampleImageService.GetAllSampleImages(userId);
            return samplesResult.Match(
                samples => Ok(samples),
                errors => Problem(errors)
            );
        }
        
        /// <summary>
        /// Returns a list of all organs in the database together with their ids and names
        /// </summary>
        [HttpGet("organ-tile-list")]
        [Authorize]
        [ProducesResponseType(typeof(OrgansListDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrganTileList()
        {
            Log.Info($"Serving route: {HttpContext.Request.Path}");

            var organsListResult = await _organService.GetListOfAllOrgans();
            return organsListResult.Match(
                organsList => Ok(organsList),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Returns a list of all diagnoses in the database with ids and names
        /// </summary>
        [HttpGet("diagnoses-list")]
        [Authorize]
        [ProducesResponseType(typeof(DiangosesListDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDiagnosisList()
        {
            var diagnosisListResult = await _diagnosisService.GetListOfAllDiagnoses();
            return diagnosisListResult.Match(
                diagnosisList => Ok(diagnosisList),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Returns organ name, description and a list of sample images with diagnoses. Used for organ detail page.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("organ-detail/{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(OrganDetailDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrganById(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            if (String.IsNullOrEmpty(userId))
            {
                Log.Warn("Returning BadRequest. User is not authenticated or failed to parse identity.");
                return BadRequest();
            }

            var organDataResult = await _organService.GetOrganDetailById(id, userId);
            return organDataResult.Match(
                organData => Ok(organData),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Used for getting a path to a specific organ icon
        /// </summary>
        /// <param name="id">id of the organ</param>
        [HttpGet("organ-icon-path/{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(IconPathDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrganIconPath(int id)
        {
            var organImage = await _organService.GetOrganIconPathById(id);
            return organImage.Match(
                organ => Ok(organ),
                errors => Problem(errors)
            );
        }


        /// <summary>
        /// Returns SampleImage for a specific image by entering the image id
        /// </summary>
        /// <param name="sampleId">id of the sample image</param>
        [HttpGet("sample-image/{sampleId:int}")]
        [Authorize]
        [ProducesResponseType(typeof(SampleImageDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSampleImage(int sampleId)

        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            if (String.IsNullOrEmpty(userId))
            {
                Log.Warn("Returning BadRequest. User is not authenticated or failed to parse identity.");
                return BadRequest();
            }
            var sampleImage = await _sampleImageService.GetSampleImageSampleId(sampleId,userId);
            return sampleImage.Match(
                sampleImage => Ok(sampleImage),
                errors => Problem(errors)
            );
        }

        [HttpPost("sample-image/{sampleId:int}/favorite")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostFavoriteSampleImage(int sampleId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            if (String.IsNullOrEmpty(userId))
            {
                Log.Warn("Returning BadRequest. User is not authenticated or failed to parse identity.");
                return BadRequest();
            }
            var result = await _sampleImageService.PostFavouiteSampleImageSampleId(userId, sampleId);
            return result.Match(
                result => Ok("Sample image was added to favorites"),
                errors => Problem(errors)
            );

        }

        [HttpDelete("sample-image/{sampleId:int}/favorite")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFavoriteSampleImage(int sampleId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            if (string.IsNullOrEmpty(userId))
            {
                Log.Warn("Returning BadRequest. User is not authenticated or failed to parse identity.");
                return BadRequest();
            }
            var result = await _sampleImageService.DeleteFavouiteSampleImageSampleId(userId,sampleId);
            return result.Match(
                result => Ok("Sample image was deleted seccessfuly"),
                errors => Problem(errors)
            );

        }

        [HttpPost("sample-image/{sampleId:int}/note")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostSampleImageNote(int sampleId, [FromBody] SampleImageNoteRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            if (string.IsNullOrEmpty(userId))
            {
                Log.Warn("Returning BadRequest. User is not authenticated or failed to parse identity.");
                return BadRequest();
            }
            var result =  await _sampleImageService.PostSampleImageNote(userId, sampleId, request.Note);
            return result.Match(
                result => Ok("Note was added to the sample image"),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Metadata of a sample image from the database
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet("sample-image/metadata/{sampleId:int}")]
        [Authorize]
        [ProducesResponseType(typeof(SampleImageMetadataDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSampleImageMetadata(int sampleId)
        {
            var metadata = await _sampleImageService.GetSampleImageMetadataById(sampleId);
            return metadata.Match(
                metadata => Ok(metadata),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Returns a list of SampleImageAnnotations for a specific image
        /// </summary>
        /// <param name="id">id of the image</param>
        [HttpGet("sample-image-annotations/{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(SampleImageAnnotationsListDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSampleImageAnnotations(int id)
        {
            // var sampleImageAnnotations = await _organService.GetSampleImageAnnotations(id);
            var annotations = await _sampleImageAnnotationService.GetSampleImageAnnotationsById(id);
            return annotations.Match(
                annotations => Ok(annotations),
                errors => Problem(errors)
            );
        }

        [HttpGet("body-system-list")]
        [Authorize]
        [ProducesResponseType(typeof(BodySystemListDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBodySystemList()
        {
            var bodySystemListResult = await _bodySystemService.GetListOfAllBodySystems();
            return bodySystemListResult.Match(
                bodySystemList => Ok(bodySystemList),
                errors => Problem(errors)
            );
        }

        [HttpGet("body-system-detail/{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(BodySystemDetailDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBodySystemById(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            if (String.IsNullOrEmpty(userId))
            {
                Log.Warn("Returning BadRequest. User is not authenticated or failed to parse identity.");
                return BadRequest();
            }

            var organDataResult = await _bodySystemService.GetBodySystemDetailById(userId, id);
            return organDataResult.Match(
                organData => Ok(organData),
                errors => Problem(errors)
            );
        }

        [HttpGet("body-system-icon-path/{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(IconPathDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBodySystemIconPath(int id)
        {
            var organImage = await _bodySystemService.GetBodySystemIconPathById(id);
            return organImage.Match(
                organ => Ok(organ),
                errors => Problem(errors)
            );
        }
    }
}