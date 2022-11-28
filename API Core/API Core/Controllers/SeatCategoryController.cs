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
    public class SeatCategoryController : ControllerBase
    {
        private iSeatCategory _iSeat;

        private readonly TicketDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<SeatCategoryController> _logger;

        public SeatCategoryController(TicketDbContext db, iSeatCategory iSeat, IMapper  mapper, ILogger<SeatCategoryController> logger)
        { 
                _db = db;
                _iSeat = iSeat;
                this._mapper = mapper;
                this._logger = logger; 

        }

        [HttpGet] 
        [Route("api/GetSeatCategory")]
        public async Task<ActionResult<IList<SeatCategoryDto>>> GetSeatCategory()
        { 
            _logger.LogInformation("Get Ticket Run using Log Information");
            try
            {
                var result = await _iSeat.GetAsync();
                var records = _mapper.Map<List<SeatCategoryDto>>(result);
                if (records == null)
                {
                    return NotFound();
                }
                return Ok(records);
            }
            catch (Exception ex) {
                _logger.LogError($"Something went wrong with {nameof(GetSeatCategory)}. Error Message: {ex.Message}.");
                return Problem($"Something went wrong with {nameof(GetSeatCategory)}. Error Message: {ex.Message}.", statusCode: 500);
            }
        }

        [HttpGet] 
        [Route("api/Ticket/GetSeatCategoryById/{id}")]
        public async Task<ActionResult<SeatCategoryDto>> GetSeatCategoryById(int id)
        {   

            var result = await _iSeat.GetAsyncId(id);
            var records = _mapper.Map<SeatCategoryDto>(result);
            if (records == null)
            {
                throw  new NotFoundException(nameof(GetSeatCategoryById),id); 
            }

            return Ok(records);

        }


        [HttpPost] 
        [Route("api/Ticket/CreateSeatCategory")]
        public async Task<IActionResult> CreateSeatCategory(SeatCategoryDto model)
        {
            try
            { 
                var records = _mapper.Map<SeatCategory>(model);
                var result = await _iSeat.CreateAsync(records);

                if (result == null)
                {
                    throw new BadRequestException(nameof(CreateSeatCategory), records.Id);
                }

                return Ok($"Added successfully");

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with {nameof(CreateSeatCategory)}. Error Message: { ex.Message}.");
                return Problem($"Something went wrong with {nameof(CreateSeatCategory)}. Error Message: {ex.Message}.", statusCode: 500);
            }
            

        }

        [HttpPatch]  
        [Route("api/Ticket/UpdateSeataCategory")]
        public async Task<IActionResult> UpdateSeataCategory(SeatCategoryDto model)
        {
            try
            {
                var records = _mapper.Map<SeatCategory>(model);
                var result = await _iSeat.UpdateAsync(records);

                if (result == null)
                {                  
                    throw new BadRequestException(nameof(UpdateSeataCategory), records.Id);
                }

                return Ok($"Category No. {records.Id} has been updated.");
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with {nameof(UpdateSeataCategory)}. Error Message: {ex.Message}.");
                return Problem($"Something went wrong with {nameof(UpdateSeataCategory)}. Error Message: {ex.Message}.", statusCode: 500);
            }
            

        }

        [HttpDelete] 
        [Route("api/Ticket/DeleteSeatCategory/{id}")]
        public async Task<IActionResult> DeleteSeatCategory(int id)
        {
            try
            {
                await _iSeat.DeleteAsyncId(id);
                var isExists = await _iSeat.Exists(id);
                if (isExists)
                {
                    return Problem($"Ticket No. {id} has not been deleted");
                }

                return Ok($"Ticket No. {id} deleted successfully");
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong with {nameof(DeleteSeatCategory)}. Error Message: {ex.Message}.");
                return Problem($"Something went wrong with {nameof(DeleteSeatCategory)}. Error Message: {ex.Message}.", statusCode: 500);
            }
        } 

    }
}
