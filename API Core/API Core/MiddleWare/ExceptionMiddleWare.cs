using API_Core.Exceptions;
using API_Core.Interface;
using API_Core.Model.Data_Transfer_Objects;
using API_Core.Model.Models;
using API_Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog.Core;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;  

namespace API_Core.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;

        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger)
        {
            _next = next;
            this._logger = logger;  
        }


        public async Task InvokeAsync(HttpContext context, Logging errorLog) {
            try
            {
                await _next(context);
            }
            catch (Exception ex) {
                _logger.LogError(ex,$"Something went wrong while processing {context.Request.Path} ");
                await HandleExceptionAsync(context, ex, errorLog);
            }
        }
         
        private Task HandleExceptionAsync(HttpContext _context, Exception ex, Logging _errorLog) {
            _context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
             

            var errorDetails = new ErrorDetails
            {
                ErrorType = "Failure",
                ErrorMessage = ex.Message,
                StatusCode = statusCode,
                UserId = _context.User.Identity.Name,
            DateLogged = DateTime.Now,
            };

            switch (ex) {
                case NotFoundException notFoundException:
                    statusCode= HttpStatusCode.NotFound;
                    errorDetails.ErrorType = "Not Found";
                    errorDetails.StatusCode = HttpStatusCode.NotFound;
                    break;

                case BadRequestException badRequestException:
                    statusCode=HttpStatusCode.BadRequest;
                    errorDetails.ErrorType = "Bad Request";
                    errorDetails.StatusCode = HttpStatusCode.BadRequest;
                    break;
                     

                default:
                    break;
            }

            //create a logging to database 
            try
            { 
                _errorLog.Log(errorDetails);
            }
            catch (Exception exc)
            {
                _logger.LogError($"An error has been encountered during database logging: {exc.Message}");
            }

            //Map to Error Details to DTO
            var errorDetailsResult = new ErrorDetailsDto
            {
                ErrorType = errorDetails.ErrorType,
                ErrorMessage = errorDetails.ErrorMessage
            };

            string response = JsonConvert.SerializeObject(errorDetailsResult);
            _context.Response.StatusCode = (int)statusCode;

            
            return _context.Response.WriteAsync(response);  
        }
    }

    
}
