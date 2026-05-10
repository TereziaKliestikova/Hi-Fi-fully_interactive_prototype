using Microsoft.AspNetCore.Mvc;
using HIPA_BE.Services.SampleImageServices;
using Microsoft.AspNetCore.Authorization;
using HIPA_BE.Contracts.Admin;
using System.Security.Claims;
using HIPA_BE.Models.Admin.SampleImageModels;
using HIPA_BE.Models.OrganModels;
using HIPA_BE.Data.Seeding;
using System.Collections;
using ErrorOr;

namespace HIPA_BE.Controllers.Admin
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("admin/sample-images")]
    public class SampleImagesController : ApiController
    {
        private readonly SampleImageService _sampleImageService;

        public SampleImagesController(SampleImageService sampleImageService)
        {
            _sampleImageService = sampleImageService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<SampleImageAdminDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSampleImages()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

            var result = await _sampleImageService.GetAllSampleImagesForAdmin(userId);
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }

        [HttpPatch("batch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ModifySampleImages([FromBody] ModifySampleImageRequest request)
        {
            var result = await _sampleImageService.ModifySampleImages(request.IDs, request.Action);
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }

        [HttpDelete("batch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSampleImages([FromBody] DeleteSampleImageRequest request)
        {
            var result = await _sampleImageService.DeleteSampleImages(request.IDs);
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }

        [HttpPost("{id:int}/flag")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> FlagSampleImage(int id, [FromBody] FlagSampleImageRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var result = await _sampleImageService.FlagSampleImage(id, request.FlagTypeId, userId);
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }

        [HttpDelete("{id:int}/flag")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UnFlagSampleImage(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var result = await _sampleImageService.UnFlagSampleImage(id, userId);
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }


        [HttpGet("keywords/{id:}")]
        [ProducesResponseType(typeof(List<String>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAutoCompletKeyWords(int id)
        {
            var result = await _sampleImageService.GetUniqueSampleImagesKeywords(id);
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }
        // [HttpGet("uploadId")]
        // [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        // public async Task<IActionResult> GetUploadGroupID()
        // {
        //     var result = await _sampleImageService.CreateSampleImageOnUpload();
        //     return result.Match(
        //         success => Ok(success),
        //         error => Problem(error)
        //     );
        // }
    }
}