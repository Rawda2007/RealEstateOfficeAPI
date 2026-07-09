using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateOfficeBusinessLogic;

namespace RealEstateOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernorateController : ControllerBase
    {
        private readonly GovernorateBL governorateBL;

        public GovernorateController()
        {
            governorateBL =
            new GovernorateBL();
        }

        [HttpGet("All")]
        public IActionResult GetAll()
        {
            return Ok(
                governorateBL.GetAll()
            );
        }
    }
}
