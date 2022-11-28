using API_Core.DBContext;
using API_Core.Exceptions;
using API_Core.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Core.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TicketDbContext _context;

        public GenericRepository(TicketDbContext context)
        {
            this._context = context;
        } 
        public async Task<T> CreateAsync(T model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;

        }

        public async  Task DeleteAsyncId(int id)
        {
            var findById = await GetAsyncId(id);
            if (findById == null)
            {
                throw new NotFoundException(nameof(DeleteAsyncId), id);
            }

            _context.Set<T>().Remove(findById);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> Exists(int? id)
        {
            var entity = await GetAsyncId(id);
            return entity != null;
        }

        public async Task<List<T>> GetAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsyncId(int? id)
        {
            if (id is null) {
                return null;
            }
            return await _context.Set<T>().FindAsync(id);
        }   

        public async Task<T> UpdateAsync(T model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
