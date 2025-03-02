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
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        private readonly MVCProjectDbContext _dbContext;

        public EmployeeRepository(MVCProjectDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _dbContext.Employees.Where(E=>E.Address == address);
        }

        public IQueryable<Employee> SearchByName(string SearchName)
        {
            return _dbContext.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(SearchName.ToLower()));
        }
    }
}
