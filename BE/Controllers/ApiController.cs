using ErrorOr;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace HIPA_BE.Controllers
{
    public class ApiController : ControllerBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(ApiController));
        /// <summary>
        /// This method is used to translate errors to status codes
        /// </summary>
        /// <param name="errors">
        /// </param>
        /// <returns>
        /// Problem object with the status code and title unpacked from the first error in the list
        /// </returns>
        [NonAction]
        public IActionResult Problem(List<Error> errors)
        {
            var firstError = errors[0];

            var statusCode = firstError.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
                _ => firstError.NumericType
            };
            return Problem(statusCode: statusCode, title: firstError.Description);
        }
    }
}