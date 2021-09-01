using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAsyncService<T> where T : Entity
    {
        Task<int> CreateAsync(T entity);
        Task<T> ReadAsync(int id);
        Task<IEnumerable<T>> ReadAsync();
        Task<bool> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
    }
}
