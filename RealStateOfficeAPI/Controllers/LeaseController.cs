using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateOfficeBusinessLogic;
using RealStateOfficeModels.Transactions;

namespace RealEstateOfficeAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaseController : ControllerBase
    {
        private readonly LeaseBL leaseBL = new LeaseBL();
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(leaseBL.GetAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var lease =
                leaseBL.GetByID(id);

            if (lease == null)
                return NotFound();

            return Ok(lease);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(LeaseModel lease)
        {
            bool result =
                leaseBL.Add(lease);

            if (!result)
                return BadRequest();

            return Ok("Lease Added Successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult Update(LeaseModel lease)
        {
            bool result =
                leaseBL.Update(lease);

            if (!result)
                return BadRequest();

            return Ok("Lease Updated Successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result =
                leaseBL.Delete(id);

            if (!result)
                return BadRequest();

            return Ok("Lease Deleted Successfully");
        }

        [Authorize(Roles = "Client")]
        [HttpGet("MyLeases")]
        public IActionResult GetMyLeases()
        {
            int clientID =
                Convert.ToInt32(
                User.FindFirst("UserID")!.Value);

            return Ok(
                leaseBL.GetMyLeases(clientID));
        }


        [Authorize(Roles = "Client")]
        [HttpGet("MyLeases/{id}")]
        public IActionResult GetMyLease(int id)
        {
            int clientID =
                Convert.ToInt32(
                User.FindFirst("UserID")!.Value);

            var lease =
                leaseBL.GetMyLease(
                id,
                clientID);

            if (lease == null)
                return NotFound();

            return Ok(lease);
        }
    }
}
