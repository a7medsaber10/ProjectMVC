using Microsoft.EntityFrameworkCore;
using ProjectMVC.BLL.Interfaces;
using ProjectMVC.DAL.Data.Contexts;
using ProjectMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        // readony --> prevent making instanse from dbContext in any method manually 
        private readonly AppDbContext _dbContext; // NULL

        public DepartmentRepository(AppDbContext dbContext) // Ask CLR for creating object from DbContext
        {
            //_dbContext = new AppDbContext(); // With this line the exception is ended
            _dbContext = dbContext;
        }

        public int Add(Department department)
        {
            _dbContext.Departments.Add(department); // null reference exception
            return _dbContext.SaveChanges(); // will make sql script for all changes 
        }

        public int Delete(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _dbContext.Departments.AsNoTracking().ToList();
        }

        public Department GetById(int id)
        {
            return _dbContext.Departments.Find(id);
        }

        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }
    }
}
