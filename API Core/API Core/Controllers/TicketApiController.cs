using API_Core.DBContext;
using API_Core.Interface;
using API_Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_Core.Controllers
{
    [ApiController]
    public class TicketApiController : ControllerBase
    {
        private iTicket _iTicket;

        private readonly TicketDbContext _db;

        public TicketApiController(TicketDbContext db, TicketData iTicket)
        {
            _db = db;
            _iTicket = iTicket;
        }

        [HttpGet]
        [Route("api/Ticket/GetTicket")]
        public IActionResult GetTicket()
        {
            var result = _iTicket.GetTicket();
            return Ok(result);
        }

        [HttpGet]
        [Route("api/Ticket/GetTicketById/{id}")]
        public IActionResult GetTicketById(int id)
        {
            var result = _iTicket.GetTicketById(id);
            if (result != null) {

                return Ok(result);
            }
            return NotFound($"Ticket No. {id} cannot be found.");
        }
        [HttpDelete]
        [Route("api/Ticket/DeleteTicketById/{id}")]
        public IActionResult DeleteTicketById(int id)
        {
            var result = _iTicket.GetTicketById(id);
            if (result != null) {
                var returnValue = _iTicket.DeleteTicketById(result);
                if (returnValue == 1) {
                    return Ok($"Ticket No. {id} deleted successfullt");
                }

            }
            return NotFound($"Ticket No.{ id } has not been found.");
        }
        [HttpPost] 
        [Route("api/Ticket/ModifyTicket")]
        public IActionResult ModifyTicket(Ticket model) {
            var result = _iTicket.ModifyTicket(model);

            if (result > 0)
            {
                if (result != model.Id)
                {

                    return Ok($"Ticket No. { result } added successfullt");
                }
                else {

                    return Ok($"Ticket No. {model.Id} has been updated.");
                }
            }
            else
            {
            return BadRequest($"Failed to add or update transaction.");
            }
        
        }
    }
}
