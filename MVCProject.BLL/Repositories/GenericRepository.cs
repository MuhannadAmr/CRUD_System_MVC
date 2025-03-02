using Microsoft.EntityFrameworkCore;
using MVCProject.BLL.Interfaces;
using MVCProject.DAL.Contexts;
using MVCProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MVCProjectDbContext _dbContext;

        public GenericRepository(MVCProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _dbContext.Employees.Include(e => e.Department).ToListAsync();
            }
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _dbContext.Employees.Include(e => e.Department).FirstOrDefaultAsync(E => E.Id == id) as T;
            }
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }

    }
}
