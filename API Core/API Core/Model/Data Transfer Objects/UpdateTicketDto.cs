using API_Core.Model.Models;

namespace API_Core.Model.Data_Transfer_Objects
{
    public class UpdateTicketDto:TicketDto
    { 
        public int Id { get; set; }
        public int SeatCategoryId { get; set; }
        public virtual SeatCategory SeatCategory { get; set; }


    }
}
