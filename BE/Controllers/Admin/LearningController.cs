using System.Reflection;
using System.Security.Claims;
using HIPA_BE.Contracts.Admin.Learning;
using HIPA_BE.Contracts.Generic;
using HIPA_BE.Models.Admin.SampleImageModels;
using HIPA_BE.Models.LearningModels;
using HIPA_BE.Models.PdfFileModels;
using HIPA_BE.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIPA_BE.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("learning")]
public class LearningController : ApiController
{
    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(AdminController));
    private readonly LearningService _learningService;

    
    public LearningController(LearningService learningService)
    {
        _learningService = learningService;
    }

    [HttpPost("directories/new")]
    [ProducesResponseType(typeof(DirectoryCreatedResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateRootDirectory([FromBody] NewRootDirectoryRequest request)
    {
        Log.Debug($"Received: {request}");
        var capitalizedCategory = char.ToUpper(request.StudyCategory[0]) + request.StudyCategory[1..];
        var success = StudyCategory.TryParse(capitalizedCategory, out StudyCategory study);

        if (!success)
            return Problem();
        
        var result = await _learningService.CreateRootDirectory(request.Name, study);

        return result.Match(
            success => Ok(success),
            Problem
        );
    }
    
    [HttpPost("directory/{id}/new")]
    [ProducesResponseType(typeof(DirectoryCreatedResponse),StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDirectory([FromRoute] int id, [FromBody] NewDirectoryRequest request)
    {
        Log.Debug($"Received: {request}");
        var result = await _learningService.CreateDirectory(id, request.Name);
        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }
    
    [HttpGet("directory/{id}/parents")]
    [ProducesResponseType(typeof(List<ItemWithNameDto>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDirectoryNotPublicParentNames([FromRoute] int id)
    {
        var result = await _learningService.GetDirectoryNotPublicParentNames(id);
        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }
    
    [HttpPatch("directory/{id}/visibility")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeDirectoryVisibility([FromRoute] int id, [FromBody] ChangeDirectoryVisibilityRequest request)
    {
        Log.Debug($"Received: {request}");
        var result = await _learningService.ChangeDirectoryVisibility(id, request.IsPublic);
        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }
    
        
    [HttpPatch("directory/{id}/update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateDirectoryInfo([FromRoute] int id, [FromBody] UpdateDirectoryInfoRequest request)
    {
        Log.Debug($"Received: {request}");
        var result = await _learningService.UpdateDirectoryInfo(id, request);
        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }
    
    [HttpDelete("directory/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteDirectory([FromRoute] int id)
    {
        var result = await _learningService.DeleteDirectory(id);
        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }
    
    [HttpDelete("directories/file/{fileId}")]
    [ProducesResponseType(typeof(PdfFileDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteFile([FromRoute] int fileId)
    {
        var result = await _learningService.DeleteFile(fileId);
        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }
    
    [HttpGet("directories/{studyCategory}")]
    [ProducesResponseType(typeof(List<DirectoryTreeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDirectoryTree([FromRoute] string studyCategory)
    {
        Log.Debug($"Received: {studyCategory}");
        var capitalizedCategory = char.ToUpper(studyCategory[0]) + studyCategory[1..];
        var success = StudyCategory.TryParse(capitalizedCategory, out StudyCategory study);
        Log.Debug($"Received: {success}");

        if (!success)
            return Problem();
        
        var result = await _learningService.GetDirectoryTreeForStudy(study, true);
        Log.Debug($"Received: {result}");

        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }
    
    [HttpGet("directoriy/{id}/detail")]
    [ProducesResponseType(typeof(DirectoryDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDirectoryDetails([FromRoute] int id)
    {
        var result = await _learningService.GetDirectoryDetails(id, true);
        Log.Debug($"Received: {result}");

        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }
    
    [HttpGet("directory/{id}/sample-images")]
    [ProducesResponseType(typeof(List<SampleImageAdminDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDirectorySampleImages([FromRoute] int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

        var result = await _learningService.GetDirectorySampleImages(id, userId);
        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }

    [HttpGet("directory/{id}/files")]
    [ProducesResponseType(typeof(List<PdfFileDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDirectoryFiles([FromRoute] int id)
    {
        var result = await _learningService.GetDirectoryFiles(id);
        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }
    
    [HttpPost("directory/{id}/sample-images")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddSampleImagesToDirectory([FromRoute] int id, [FromBody] BulkSampleImageRequest request)
    {
        var result = await _learningService.AddSampleImagesToDirectory(id, request.SampleImageIds);
        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }
    
    [HttpDelete("directory/{id}/sample-images")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteSampleImagesFromDirectory([FromRoute] int id, [FromBody] BulkSampleImageRequest request)
    {
        var result = await _learningService.DeleteSampleImagesFromDirectory(id, request.SampleImageIds);
        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }
    
}