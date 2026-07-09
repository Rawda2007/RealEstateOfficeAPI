using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateOfficeBusinessLogic;

namespace RealEstateOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurposeController : ControllerBase
    {
        [HttpGet("All")]
        public IActionResult GetAll()
        {
            return Ok(
                PurposeBL.GetAll()
            );
        }
    }
}
