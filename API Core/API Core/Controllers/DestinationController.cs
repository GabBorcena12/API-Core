using API_Core.DBContext;
using API_Core.Interface;
using API_Core.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Core.Controllers
{
    [ApiController]
    public class DestinationController : ControllerBase
    {
        private IDestination _iDestination;

        private readonly TicketDbContext _db;

        public DestinationController(TicketDbContext db, IDestination iDestination)
        {
            _db = db;
            _iDestination = iDestination;
        }
        [HttpGet]
        [Route("api/GetDestinations")]
        public IActionResult GetDestinations()
        {
            var query = _iDestination.GetDestinations();
            if (query != null)
            {
                return Ok(query);
            }
            return NotFound($"No data available");
        }
        [HttpGet]
        [Route("api/GetDestinations/{id}")]
        public IActionResult GetDestinationsById(int id)
        {
            var query = _iDestination.GetDestinationsById(id);
            if (query != null)
            {
                return Ok(query);
            }
            return NotFound($"Id No: { id } not found.");
        }
        [HttpPost]
        [Route("api/ModifyDestinations")]
        public IActionResult ModifyDestinations(TouristDestination model)
        {
            var result = _iDestination.ModifyDestinations(model);
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
        [Route("api/DeleteDestination/{id}")]
        public IActionResult DeleteDestination(int id)
        {
            var result = _iDestination.GetDestinationsById(id);
            if (result != null)
            {
                var returnValue = _iDestination.DeleteDestinations(result);
                if (returnValue == 1)
                {
                    return Ok($"Id No: {id} deleted successfullt");
                }

            }
            return NotFound($"Id No:{ id } has not been found.");
        }
    }
}
