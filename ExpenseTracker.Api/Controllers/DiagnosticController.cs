using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("ping/secure")]
        public IActionResult PingSecure()
        {
            return this.Ok("all is secured");
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return this.Ok("Service is working");
        }
    }
}