using Microsoft.AspNetCore.Mvc;

namespace HIPA_BE.Controllers
{
    public class ErrorController : ControllerBase
    {

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}