using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetByNameAsync(string name);
        Task AddAsync(T value);
        Task<bool> UpdateAsync(T value);
        Task<bool> DeleteAsync(int id);
    }
}
