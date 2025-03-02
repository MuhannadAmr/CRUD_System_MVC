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
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        private readonly MVCProjectDbContext _dbContext;

        public DepartmentRepository(MVCProjectDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Department> GetByName(string SearchValue)
        {
            return _dbContext.Departments.Where(D => D.Name.ToLower().Contains(SearchValue.ToLower()));
        }
    }
}
