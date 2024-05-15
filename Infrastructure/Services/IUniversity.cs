using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUniversity
    {
        IEnumerable<T> GetAll<T>(int pageSize, int pageNumber) where T : class;

        T FindByIdAsync<T>(int id) where T : class;

        void AddAsync<T>(T entity) where T : class;

        void RemoveAsync<T>(T entity) where T : class;

        void UpdateAsync<T>(T entity) where T : class;
    }
}
