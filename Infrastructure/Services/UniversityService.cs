using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.Services
{
    public class UniversityService : IUniversity
    {
        private readonly AppDbContext _context;

        public UniversityService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAsync<T>(T entity) where T : class
        {
            _context.Add<T>(entity);
            _context.SaveChanges();
        }

        public T FindByIdAsync<T>(int id) where T : class => _context.Find<T>(id);
        public IEnumerable<T> GetAll<T>(int pageSize, int pageNumber) where T : class
        {
            return _context.Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public void RemoveAsync<T>(T entity) where T : class
        {
            _context.Remove<T>(entity);
            _context.SaveChanges();
        }

        public void UpdateAsync<T>(T entity) where T : class
        {
            _context.Update<T>(entity);
            _context.SaveChanges();
        }
    }
}
