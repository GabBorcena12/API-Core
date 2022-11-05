using API_Core.DBContext;
using Microsoft.AspNetCore.Mvc;
using API_Core.Interface;
using API_Core.Model.Models;

namespace API_Core.Controllers
{
    [ApiController]
    public class PriceApiController : ControllerBase
    {
        private IPrice _iPrice;

        private readonly TicketDbContext _db;

        public PriceApiController(TicketDbContext db, IPrice iPrice)
        {
            _db = db;
            _iPrice = iPrice;
        }

        [HttpGet]
        [Route("api/GetTicketPrice")]
        public IActionResult GetTicketPrice()
        {
            var query = _iPrice.GetTicketPrice();
            if (query != null) {
                return Ok(query);
            }
            return NotFound($"No data available");
        }
        [HttpGet]
        [Route("api/GetTicketPrice/{id}")]
        public IActionResult GetTicketPriceById(int id)
        {
            var query = _iPrice.GetTicketPriceById(id);
            if (query != null)
            {
                return Ok(query);
            }
            return NotFound($"Id No: { id } not found.");
        }
        [HttpPost]
        [Route("api/ModifyTicketPrice")]
        public IActionResult ModifyTicketPrice(TicketPrice model)
        {
            var result = _iPrice.ModifyTicketPrice(model);
            if (result > 0)
            {
                if (result != model.Id)
                {

                    return Ok($"Id No: { result } added successfullt");
                }
                else
                {

                    return Ok($"Id No: {model.Id} has been updated.");
                }
            }
            else
            {
                return BadRequest($"Transaction failed.");
            }
        }
        [HttpDelete]
        [Route("api/DeleteTicketPrice/{id}")]
        public IActionResult DeleteTicketPrice(int id)
        {
            var result = _iPrice.GetTicketPriceById(id);
            if (result != null)
            {
                var returnValue = _iPrice.DeleteTicketPrice(result);
                if (returnValue == 1)
                {
                    return Ok($"Id No: {id} deleted successfullt");
                }

            }
            return NotFound($"Id No:{ id } has not been found.");
        }
    }
}
