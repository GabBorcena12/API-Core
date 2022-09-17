using API_Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Core.Interface
{
    public interface iTicket
    {
        public List<Ticket> GetTicket();
        public Ticket GetTicketById(int id);
        public int ModifyTicket(Ticket model);
        public int DeleteTicketById(Ticket model);
    }
}
