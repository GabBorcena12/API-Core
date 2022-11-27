using API_Core.Model;
using API_Core.Model.Data_Transfer_Objects;
using API_Core.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Core.Interface
{
    public interface iSeatCategory
    {
        public List<SeatCategoryDto> GetAsync();
        public SeatCategoryDto GetAsyncId(int id);
        public int CreateAsync(SeatCategoryDto model);
        public int UpdateAsync(SeatCategoryDto model);
        public  int DeleteAsyncId(int id);
    }
}
