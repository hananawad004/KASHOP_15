using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace KASHOP.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
         private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<T>> GetAsync(CancellationToken cancellationToken = default) => await _context.Set<T>().ToListAsync(cancellationToken);
    }
}
