using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateOfficeBusinessLogic;

namespace RealEstateOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        public AreaBL areaBL=new AreaBL();
        [HttpGet("All")]
        public IActionResult GetAll()
        {
            return Ok(
                areaBL.GetAll()
            );
        }

        [HttpGet("{id}")]
        public IActionResult GetByGovernorate(int id)
        {
            return Ok(
                areaBL.GetByGovernorate(id)
            );
        }
    }
}
