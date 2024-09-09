using Microsoft.EntityFrameworkCore;
using ProjectMVC.BLL.Interfaces;
using ProjectMVC.DAL.Data;
using ProjectMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.BLL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        // readony --> prevent making instanse from dbContext in any method manually 
        private readonly AppDbContext _dbContext; // NULL

        public EmployeeRepository(AppDbContext dbContext) // Ask CLR for creating object from DbContext
        {
            //_dbContext = new AppDbContext(); // With this line the exception is ended
            _dbContext = dbContext;
        }

        public int Add(Employee employee)
        {
            _dbContext.Employees.Add(employee); // null reference exception
            return _dbContext.SaveChanges(); // will make sql script for all changes 
        }

        public int Delete(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees.AsNoTracking().ToList();
        }

        public Employee GetById(int id)
        {
            return _dbContext.Employees.Find(id);
        }

        public int Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            return _dbContext.SaveChanges();
        }
    }
}
