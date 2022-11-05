using System;

namespace API_Core.Model.Data_Transfer_Objects
{
    public abstract class BaseTicketDto
    {
        public string Description { get; set; }
        public int? TotalSeats { get; set; }
        public int? AvailableSeats { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? StreamingDateTime { get; set; }
    }

}
