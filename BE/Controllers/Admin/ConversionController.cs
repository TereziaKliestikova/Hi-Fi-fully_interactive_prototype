using HIPA_BE.Models.SampleImageModels;
using HIPA_BE.Services;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;

namespace HIPA_BE.Controllers.Admin
{
    [ApiController]
    [Route("conversion")]
    public class ConversionController : ApiController
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(ConversionController));
        private readonly ConversionService _conversionService;

        public ConversionController(ConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        // Basic Authentication https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Authorization
        private bool BasicAuthenticationCheck(HttpContext context)
        {
            string? authHeader = context.Request.Headers.Authorization;
            if (authHeader == null || !authHeader.StartsWith("Basic "))
            {
                return false;
            }
            
            var encodedCredentials = authHeader["Basic ".Length..].Trim();
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));

            var parts = credentials.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }
            var username = parts[0];
            var password = parts[1];

            return username == "conversion_user" && password == "Kosela_S_Bobrami_123_!";
        }

        [HttpGet("job")]
        [ProducesResponseType(typeof(List<ConversionJobDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPendingConversionJobs()
        {
            if (!BasicAuthenticationCheck(HttpContext))
            {
                return Unauthorized();
            }

            _log.Info("Passed auth check and fetching pending conversion jobs.");
            var result = await _conversionService.GetAllPendingConversionJobs();

            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }

        [HttpPatch("job/{sampleImageId:int}")]
        [ProducesResponseType(typeof(ConversionJobDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateConversionJob(int sampleImageId, ConversionJobDto payload)
        {
            if (!BasicAuthenticationCheck(HttpContext))
            {
                return Unauthorized();
            }

            _log.Info($"Received conversion job update request and passed auth check.");
            if (payload == null)
            {
                _log.Error("Payload is null.");
                return BadRequest("Payload cannot be null.");
            }
            _log.Info($"With data SampleImageId: {sampleImageId} and payload: {payload}");
            var result = await _conversionService.UpdateConversionJob(sampleImageId, payload);

            return result.Match(
                success => Ok(success),
                error => Problem(error)
            );
        }
    }
}
