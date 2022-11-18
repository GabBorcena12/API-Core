using API_Core.DBContext;
using API_Core.Exceptions;
using API_Core.Interface;
using API_Core.Model;
using API_Core.Model.Data_Transfer_Objects;
using API_Core.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
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
        private readonly ILogger<TicketApiController> _logger;

        public TicketApiController(TicketDbContext db, TicketData iTicket, IMapper  mapper, ILogger<TicketApiController> logger)
        { 
                _db = db;
                _iTicket = iTicket;
                this._mapper = mapper;
                this._logger = logger; 

        }

        [HttpGet]
        //[Authorize]
        [Route("api/Ticket/GetTicket")]
        public ActionResult<IEnumerable<GetTicketDto>> GetTicket()
        { 
            _logger.LogInformation("Get Ticket Run using Log Information");
            try
            {
                var result = _iTicket.GetAsync();
                return Ok(result);
            }
            catch (Exception ex) {
                _logger.LogError($"Something went wrong with {nameof(GetTicket)}. Error Message: {ex.Message}.");
                return Problem($"Something went wrong with {nameof(GetTicket)}. Error Message: {ex.Message}.", statusCode: 500);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/Ticket/GetTicketById/{id}")]
        public ActionResult<GetTicketDto> GetTicketById(int id)
        {   

            var result = _iTicket.GetAsyncId(id);

            if (result == null)
            {
                //var errorThrow =  new NotFoundException(nameof(GetTicketById),id);
                //Serilog.Log.Error($"Something went wrong with {nameof(GetTicket)} ==> Get Method Error Details: {0}");
                return NotFound($"Ticket id {id} not found.");
            }

            return Ok(result);

        }


        [HttpPost]
        [Authorize(Roles = "Administrator,User")]
        [Route("api/Ticket/CreateTicket")]
        public IActionResult CreateTicket(CreateTicketDto model)
        {
            try
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
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with {nameof(CreateTicket)}. Error Message: { ex.Message}.");
                return Problem($"Something went wrong with {nameof(CreateTicket)}. Error Message: {ex.Message}.", statusCode: 500);
            }
            

        }

        [HttpPatch] 
        [Authorize(Roles = "Administrator,User")]
        [Route("api/Ticket/UpdateTicket")]
        public IActionResult UpdateTicket(UpdateTicketDto model)
        {
            try
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
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with {nameof(UpdateTicket)}. Error Message: {ex.Message}.");
                return Problem($"Something went wrong with {nameof(UpdateTicket)}. Error Message: {ex.Message}.", statusCode: 500);
            }
            

        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [Route("api/Ticket/DeleteTicketById/{id}")]
        public  IActionResult DeleteTicketById(int id)
        {
            try
            {
                var result = _iTicket.DeleteAsyncId(id);
                if (result != 1)
                {
                    //throw new NotFoundException(nameof(DeleteTicketById), id);

                    _logger.LogError($"Something went wrong with {nameof(DeleteTicketById)}.");
                    return NotFound($"Ticket id {id} cannot be deleted.");
                }

                return Ok($"Ticket No. {id} deleted successfully");
            }

            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with {nameof(DeleteTicketById)}. Error Message: {ex.Message}.");
                return Problem($"Something went wrong with {nameof(DeleteTicketById)}. Error Message: {ex.Message}.", statusCode: 500);
            }
        }

        /////////////////////////////Async////////////////////////////////////////
        [NonAction]
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
        [NonAction]
        [HttpGet]
        [Route("api/Ticket/GetAsyncTicket")]
        public async Task<ActionResult<IEnumerable<GetTicketDto>>> GetAsyncTicket()
        {
            var result = await _db.tblTicket.ToListAsync();
            return Ok(result);
        }


    }
}
