using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ReKreator.UI.MVC.Controllers
{
    public class ErrorController : Controller
    {
        private ILogger<ErrorController> Logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            Logger = logger;
        }

        public IActionResult Error500(bool WithoutLog)
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewData["StatusCode"] = 500;
            ViewData["Message"] = "Server can't handle this query. Please try again later.";

            if (exceptionFeature != null && !WithoutLog)
            {
                Logger.LogError(exceptionFeature.Error, exceptionFeature.Error.Message);
            }

            return View("Error");
        }

        public IActionResult Error404()
        {
            ViewData["StatusCode"] = 404;
            ViewData["Message"] = "Resource not found.";
            return View("Error");
        }
    }
}