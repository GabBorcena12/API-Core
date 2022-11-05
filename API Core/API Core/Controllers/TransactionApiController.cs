using API_Core.Interface;
using API_Core.Model.Models;
using API_Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_Core.Controllers
{
    [ApiController]
    public class TransactionApiController : ControllerBase
    {
        private readonly iTransaction _iTransaction;
        private readonly Transaction _Transaction;  
        private readonly SeatData _Seat;
        public TransactionApiController(iTransaction iTransaction, Transaction transaction, SeatData seat)
        {
            _iTransaction = iTransaction;
            _Transaction = transaction;
            _Seat = seat;   
        }
        [HttpGet]
        [Route("api/Transaction/GetTransactions")]
        public IActionResult GetTransactions()
        {
            var result = _iTransaction.GetTransactions();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"Data not available.");
        }
        [HttpGet]
        [Route("api/Transaction/GetTransactionsById/{id}")]
        public IActionResult GetTransactionsById(int id)
        {
            var result = _iTransaction.GetTransactionsById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"Data not available.");
        }
        [HttpPost]
        [Route("api/Transaction/ModifyTransaction")]
        public IActionResult ModifyTransaction(Transact model)
        {
            var result = _iTransaction.ModifyTransaction(model);
            if (result > 0)
            {
                if (result != model.Id){
                    return Ok($"Transaction No. { result } added successfullt");
                }
                else{
                    return Ok($"Transaction No. {model.Id} has been updated.");
                }
            }
            else {
                if (result == -1){
                    return BadRequest($"Seat capacity has been reach.");
                }
                else{
                    return BadRequest($"Failed to add or update transaction.");
                }
            }
        }
        [HttpDelete]
        [Route("api/Transaction/DeleteTransaction/{id}")]
        public IActionResult DeleteTransaction(int id) { 
            var result = _iTransaction.DeleteTransaction(id);
            if (result > 0) { 
                return Ok($"Transaction No. { result } has been deleted.");
            }

            return BadRequest($"Cannot delete transaction no. { id }.");
        }

        [HttpGet]
        [Route("api/CheckAvailableSeat/{seatid}/{id}")]
        public IActionResult CheckAvailableSeat(int seatid,int id)
        {
            var query = _Seat.CheckAvailableSeat(seatid, id,null,false);
            return Ok(query);
        }
    }
}
