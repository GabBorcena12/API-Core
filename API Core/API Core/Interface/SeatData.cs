using API_Core.DBContext;
using API_Core.Model;
using System.Linq;

namespace API_Core.Interface
{
    public class SeatData : ISeat
    {
        private readonly TicketDbContext _db;
        public SeatData(TicketDbContext db)
        {
            _db = db;
        }
        public int CheckAvailableSeat(int id, int? toAvailQty, int? transId, bool? forUpdate)
        {
            var seat = _db.tblTicketSeats.Find(id);
            var query = _db.tblTransaction.Find(transId);
            if (seat.Capacity > 0)
            {
                //get available seat
                var trans = _db.tblTransaction.Where(x => x.SeatId == id).Sum(x => x.AcquiredSeats);
                var available = seat.Capacity - trans;

                if ((forUpdate == false && available >= toAvailQty) ||  
                (forUpdate == true && available <= 0 && query.AcquiredSeats >= toAvailQty) ||
                (forUpdate == true && available > 0 && (trans - query.AcquiredSeats) >= toAvailQty))
                {
                    return 1;
                }
                else {
                    return 0;
                }
            }
            else {
                return 0;
            } 
        }
    }
}
