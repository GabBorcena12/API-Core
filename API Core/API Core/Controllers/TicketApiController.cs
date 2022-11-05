using API_Core.DBContext;
using API_Core.Interface;
using API_Core.Model;
using API_Core.Model.Data_Transfer_Objects;
using API_Core.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Core.Controllers
{
    [ApiController]
    public class TicketApiController : ControllerBase
    {
        private iTicket _iTicket;

        private readonly TicketDbContext _db;
        private readonly IMapper _mapper;

        public TicketApiController(TicketDbContext db, TicketData iTicket, IMapper  mapper)
        {
            _db = db;
            _iTicket = iTicket;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("api/Ticket/GetTicket")]
        public ActionResult<IEnumerable<GetTicketDto>> GetTicket()
        {
            var result = _iTicket.GetAsync();
            return  Ok(result);
        }

        [HttpGet]
        [Route("api/Ticket/GetTicketById/{id}")]
        public ActionResult<GetTicketDto> GetTicketById(int id)
        { 
            var result = _iTicket.GetAsyncId(id);

            if (result == null) {

                return NotFound($"Ticket No. {id} cannot be found.");
            }

            return Ok(result);

        }


        [HttpPost]
        [Route("api/Ticket/CreateTicket")]
        public IActionResult CreateTicket(CreateTicketDto model)
        {
            var result = _iTicket.CreateAsync(model);

            if (result > 0)
            {
                return Ok($"Ticket No. {result} added successfullt");
            }
            else
            {
                return BadRequest($"Failed to add ticket.");
            }

        }

        [HttpPatch]
        [Route("api/Ticket/UpdateTicket")]
        public IActionResult UpdateTicket(UpdateTicketDto model)
        {
            var result = _iTicket.UpdateAsync(model);

            if (result > 0)
            {  
                return Ok($"Ticket No. {model.Id} has been updated."); 
            }
            else
            {
                return BadRequest($"Failed to update ticket.");
            }

        }
        [HttpDelete]
        [Route("api/Ticket/DeleteTicketById/{id}")]
        public  IActionResult DeleteTicketById(int id)
        { 
            var result = _iTicket.DeleteAsyncId(id);
            if (result != 1)
            {
                return NotFound($"Ticket No.{id} has not been deleted.");
            }

            return Ok($"Ticket No. {id} deleted successfully");
        }

        /////////////////////////////Async////////////////////////////////////////

        [HttpGet]
        [Route("api/Ticket/GetAsyncTicketById/{id}")]
        public async Task<ActionResult<GetTicketDto>> GetAsyncTicketById(int id)
        {
            var result = await _db.tblTicket.FindAsync(id);

            if (result == null)
            {

                return NotFound($"Ticket No. {id} cannot be found.");
            }

            return Ok(result);

        }

        [HttpGet]
        [Route("api/Ticket/GetAsyncTicket")]
        public async Task<ActionResult<IEnumerable<GetTicketDto>>> GetAsyncTicket()
        {
            var result = await _db.tblTicket.ToListAsync();
            return Ok(result);
        }


    }
}
