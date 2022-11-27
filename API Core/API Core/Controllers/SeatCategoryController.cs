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
    [NonController]
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
        //[Authorize]
        [Route("api/GetSeatCategory")]
        public ActionResult<IEnumerable<SeatCategoryDto>> GetSeatCategory()
        { 
            _logger.LogInformation("Get Ticket Run using Log Information");
            try
            {
                var result = _iSeat.GetAsync();
                return Ok(result);
            }
            catch (Exception ex) {
                _logger.LogError($"Something went wrong with {nameof(GetSeatCategory)}. Error Message: {ex.Message}.");
                return Problem($"Something went wrong with {nameof(GetSeatCategory)}. Error Message: {ex.Message}.", statusCode: 500);
            }
        }

        [HttpGet]
        //[Authorize]
        [Route("api/Ticket/GetSeatCategoryById/{id}")]
        public ActionResult<SeatCategoryDto> GetSeatCategoryById(int id)
        {   

            var result = _iSeat.GetAsyncId(id);

            if (result == null)
            {
                throw  new NotFoundException(nameof(GetSeatCategoryById),id); 
            }

            return Ok(result);

        }


        [HttpPost]
        [Authorize(Roles = "Administrator,User")]
        [Route("api/Ticket/CreateSeatCategory")]
        public IActionResult CreateSeatCategory(SeatCategoryDto model)
        {
            try
            {
                var result = _iSeat.CreateAsync(model);

                if (result > 0)
                {
                    return Ok($"Category No. {result} added successfully");
                }
                else
                {
                    throw new BadRequestException(nameof(CreateSeatCategory), model.Id); 
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with {nameof(CreateSeatCategory)}. Error Message: { ex.Message}.");
                return Problem($"Something went wrong with {nameof(CreateSeatCategory)}. Error Message: {ex.Message}.", statusCode: 500);
            }
            

        }

        [HttpPatch] 
        [Authorize(Roles = "Administrator")]
        [Route("api/Ticket/UpdateSeataCategory")]
        public IActionResult UpdateSeataCategory(SeatCategoryDto model)
        {
            try
            {
                var result = _iSeat.UpdateAsync(model);

                if (result > 0)
                {
                    return Ok($"Category No. {model.Id} has been updated.");
                }
                else
                {
                    throw new BadRequestException(nameof(UpdateSeataCategory), model.Id);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong with {nameof(UpdateSeataCategory)}. Error Message: {ex.Message}.");
                return Problem($"Something went wrong with {nameof(UpdateSeataCategory)}. Error Message: {ex.Message}.", statusCode: 500);
            }
            

        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [Route("api/Ticket/DeleteSeatCategory/{id}")]
        public  IActionResult DeleteSeatCategory(int id)
        {
            try
            {
                var result = _iSeat.DeleteAsyncId(id);
                if (result != 1)
                {
                    throw new NotFoundException(nameof(DeleteSeatCategory), id); 
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
