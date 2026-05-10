using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using log4net;
using System.Reflection;
using HIPA_BE.Models.LearningModels;
using HIPA_BE.Models.PdfFileModels;
using HIPA_BE.Models.SampleImageModels;
using HIPA_BE.Services;

namespace HIPA_BE.Controllers.Learning{

[ApiController]
[Authorize]
[Route("classwork")]

public class ClassworkController : ApiController{
    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(ClassworkController));
    private readonly LearningService _learningService;

    
    public ClassworkController(LearningService learningService)
    {
        _learningService = learningService;
    }

    [HttpGet("directories/{studyCategory}")]
    [ProducesResponseType(typeof(List<DirectoryTreeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDirectoryTree([FromRoute] string studyCategory)
    {
        var capitalizedCategory = char.ToUpper(studyCategory[0]) + studyCategory[1..];
        var success = StudyCategory.TryParse(capitalizedCategory, out StudyCategory study);
        
        if (!success)
            return Problem();
        
        var result = await _learningService.GetDirectoryTreeForStudy(study, false);

        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }

    [HttpGet("directoriy/{id}/detail")]
    [ProducesResponseType(typeof(DirectoryDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDirectoryDetails([FromRoute] int id)
    {
        var result = await _learningService.GetDirectoryDetails(id, false);
        Log.Debug($"Received: {result}");

        return result.Match(
            success => Ok(success),
            error => Problem(error)
        );
    }

    
    [HttpGet("directory/{id}/sample-images")]
    [ProducesResponseType(typeof(List<SampleImageDiagnosisDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDirectorySampleImages([FromRoute] int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

        var result = await _learningService.GetDirectorySampleImagesStudent(id, userId);
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
}
}
