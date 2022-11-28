using API_Core.DBContext;
using API_Core.Interface;
using API_Core.Model.Models;

namespace API_Core.Repository
{
    public class SeatCategoryData : GenericRepository<SeatCategory>, iSeatCategory
    {
        public SeatCategoryData(TicketDbContext context):base(context)
        {

        }
    }
}
 