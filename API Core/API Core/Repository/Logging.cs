using API_Core.Controllers;
using API_Core.DBContext;
using API_Core.Interface;
using API_Core.Model.Data_Transfer_Objects;
using API_Core.Model.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace API_Core.Repository
{
    public class Logging : ILogging
    {
        private readonly TicketDbContext _db;
        public readonly ILogger<Logging> _logger;

        public Logging(TicketDbContext db,ILogger<Logging> logger)
        {
            this._db = db;
            this._logger = logger;
        }
         
        public void Log(ErrorDetails errorDetails)
        {
            try
            {
                _db.tblErrorLogs.Add(errorDetails);
                _db.SaveChanges();
                _logger.LogInformation($"Error Message : {errorDetails.ErrorMessage } has been logged successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message has been encountered =>  {ex.Message}");
            }

        }
         
    }
}
