using Microsoft.EntityFrameworkCore;
using ProjectMVC.BLL.Interfaces;
using ProjectMVC.DAL.Data;
using ProjectMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        // readony --> prevent making instanse from dbContext in any method manually 
        //private protected readonly AppDbContext _dbContext; // NULL

        public EmployeeRepository(AppDbContext dbContext):base(dbContext) // Ask CLR for creating object from DbContext
        {
            //_dbContext = new AppDbContext(); // With this line the exception is ended
            //_dbContext = dbContext;
        }
        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
            return _dbContext.Employees.Where(E => E.Address.ToLower().Contains(address.ToLower()));
        }

        public IQueryable<Employee> GetEmployeeByName(string name)
        {
            return _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));

        }
    }
}
