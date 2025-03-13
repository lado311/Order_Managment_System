using OrderManagment.SharedLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagment.SharedLibrary.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByAsync(Expression<Func<T, bool>> predicate);

    }
}
