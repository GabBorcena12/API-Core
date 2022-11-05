using API_Core.Model;
using API_Core.Model.Data_Transfer_Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Core.Interface
{
    public interface iTicket
    {
        public List<GetTicketDto> GetAsync();
        public GetTicketDto GetAsyncId(int id);
        public int CreateAsync(CreateTicketDto model);
        public int UpdateAsync(UpdateTicketDto model);
        public  int DeleteAsyncId(int id);
    }
}
