using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateOfficeBusinessLogic;
using RealStateOfficeModels.Transactions;

namespace RealEstateOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DealController : ControllerBase
    {
        private readonly DealBL dealBL = new DealBL();


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(dealBL.GetAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var deal = dealBL.GetByID(id);

            if (deal == null)
                return NotFound();

            return Ok(deal);
        }

        [Authorize(Roles = "Client")]
        [HttpPost("Create")]
        public IActionResult Create(DealAddModel model)
        {
            int clientID =
                Convert.ToInt32(
                    User.FindFirst("UserID")!.Value);

            DealModel deal = new DealModel()
            {
                PropertyID = model.PropertyID,
                ClientID = clientID,
                Amount = model.Amount
            };

            bool result = dealBL.Add(deal);

            if (!result)
                return BadRequest();

            return Ok("Deal Created Successfully");
        }

        [Authorize(Roles = "Client")]
        [HttpGet("MyDeals")]
        public IActionResult GetMyDeals()
        {
            int clientID =
                Convert.ToInt32(
                    User.FindFirst("UserID")!.Value);

            return Ok(
                dealBL.GetMyDeals(clientID));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Approve/{id}")]
        public IActionResult Approve(int id)
        {
            bool result = dealBL.Approve(id);

            if (!result)
                return BadRequest();

            return Ok("Deal Approved Successfully");
        }


        [HttpPut("Reject/{id}")]
        public IActionResult Reject(int id)
        {
            bool result = dealBL.Reject(id);

            if (!result)
                return BadRequest();

            return Ok("Deal Rejected Successfully");
        }
        

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = dealBL.Delete(id);

            if (!result)
                return BadRequest();

            return Ok("Deal Deleted Successfully");
        }

        [Authorize(Roles = "Client")]
        [HttpPut("Cancel/{id}")]
        public IActionResult Cancel(int id)
        {
            int clientID =
                Convert.ToInt32(
                    User.FindFirst("UserID")!.Value);

            bool result =
                dealBL.CancelDeal(
                    id,
                    clientID);

            if (!result)
                return BadRequest(
                    "You can't cancel this deal.");

            return Ok(
                "Deal Cancelled Successfully");
        }
    }
}
