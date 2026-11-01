using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Repositories
{
    public interface IGenericRepository <T> where T : class
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<List<T>> GetAsync();

    }
}
