using Microsoft.AspNetCore.Mvc;
using HIPA_BE.Services;
using Microsoft.AspNetCore.Authorization;
using HIPA_BE.Contracts.Admin;
using HIPA_BE.Models.Admin.FlagModels;
using log4net;
using System.Reflection;
using HIPA_BE.Models.Admin.SampleImageModels;


namespace HIPA_BE.Controllers.Admin
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("admin")]
    public class AdminController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(AdminController));
        private readonly AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("upload-sample-image-data")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        public async Task<IActionResult> UploadSampleImageData([FromForm] StoreSampleImageDataRequest request)
        {
            Log.Debug($"Received: {request}");
            var result = await _adminService.SaveSampleImageData(request);
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }

        [HttpGet("flags")]
        [ProducesResponseType(typeof(List<FlagTypeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFlags()
        {
            var result = await _adminService.GetAllFlags();
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }

        [HttpPost("edit-sample")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> EditSampleImageInfo(SampleImageAdminDto sample, int organId)
        {
           var result=await _adminService.EditSampleImageInfo(sample, organId);
            return result.Match(
                success => Ok(),
                error => Problem(error)
            );
        }

        [HttpPost("flags")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAdminFlag([FromBody] FlagTypeDto request)
        {
           var result=await _adminService.CreateAdminFlag(request);
            return result.Match(
                success => Ok(),
                error => Problem(error)
            );
        }

        [HttpDelete("flag/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAdminFlag(int id)
        {
           var result=await _adminService.DeleteAdminFlag(flagId: id);
            return result.Match(
                success => Ok(),
                error => Problem(error)
            );
        }

        [HttpDelete("caustry/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCaustry(int id)
        {
            var result = await _adminService.DeleteCaustryFileFromSampleImage(id);
            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }
    }
}