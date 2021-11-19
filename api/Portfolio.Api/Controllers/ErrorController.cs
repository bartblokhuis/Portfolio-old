using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Portfolio.Controllers
{
    public class ErrorController : Controller
    {
        #region Methods

        [HttpGet("/Error"), HttpPost("/Error")]
        public IActionResult Error([FromQuery] int status = 400)
        {
            if (HttpContext.Request.Headers.ContainsKey("Accept") &&
                HttpContext.Request.Headers["Accept"].Contains("application/json"))
            {
                if (status == 403)
                {
                    // TODO: There is no way to get claim here!
                    return new JsonResult(new { error = "FORBIDDEN" });
                }
                else if (status == 401)
                {
                    return new JsonResult(new { error = "LOGIN_REQUIRED" });
                }
                else
                {
                    return new JsonResult(new { error = "UNDEFINED_ERROR" });
                }
            }

            return new JsonResult(new { error = "ERROR" });
            // HTML Version if you wish ...
        }

        #endregion
    }
}
