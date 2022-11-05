using API_Core.Model.Models;
using System.Collections.Generic;

namespace API_Core.Interface
{
    public interface IPrice
    {
        public List<TicketPrice> GetTicketPrice();
        public TicketPrice GetTicketPriceById(int id);
        public int ModifyTicketPrice(TicketPrice model);
        public int DeleteTicketPrice(TicketPrice model);

    }
}
