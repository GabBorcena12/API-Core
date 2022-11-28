using API_Core.DBContext;
using API_Core.Exceptions;
using API_Core.Interface;
using API_Core.Model;
using API_Core.Model.Data_Transfer_Objects;
using API_Core.Model.Models;
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
    public class TicketController : ControllerBase
    {
        private iTicket _iTicket;

        private readonly TicketDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<TicketController> _logger;

        public TicketController(TicketDbContext db, iTicket iTicket, IMapper  mapper, ILogger<TicketController> logger)
        { 
                _db = db;
                _iTicket = iTicket;
                this._mapper = mapper;
                this._logger = logger; 

        }

        [HttpGet]
        [Authorize]
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
        public ActionResult<Ticket> GetTicketById(int id)
        {   

            var result = _iTicket.GetAsyncId(id);
            
            if (result == null)
            {
                throw  new NotFoundException(nameof(GetTicketById),id); 
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
                    return Ok($"Ticket No. {result} added successfully");
                }
                else
                {
                    throw new BadRequestException(nameof(CreateTicket), model.Id); 
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
                    throw new BadRequestException(nameof(UpdateTicket), model.Id);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with {nameof(UpdateTicket)}. Error Message: {ex.Message}.");
                return Problem($"Something went wrong with {nameof(UpdateTicket)}. Error Message: {ex.Message}.", statusCode: 500);
            }
            

        }

        [HttpDelete]
        [Authorize(Roles = "Administrator,User")]
        [Route("api/Ticket/DeleteTicketById/{id}")]
        public  IActionResult DeleteTicketById(int id)
        {
            try
            {
                var result = _iTicket.DeleteAsyncId(id);
                if (result != 1)
                {
                    throw new NotFoundException(nameof(DeleteTicketById), id); 
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
