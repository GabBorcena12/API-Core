using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Core.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAsync();
        Task<T> GetAsyncId(int? id);
        Task<T> CreateAsync(T model);
        Task<T> UpdateAsync(T model);
        Task DeleteAsyncId(int id);
        Task<bool> Exists(int? id);
    }
}
