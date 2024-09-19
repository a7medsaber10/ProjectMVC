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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        // readony --> prevent making instanse from dbContext in any method manually 
        private protected readonly AppDbContext _dbContext; // NULL

        public GenericRepository(AppDbContext dbContext) // Ask CLR for creating object from DbContext
        {
            //_dbContext = new AppDbContext(); // With this line the exception is ended
            _dbContext = dbContext;
        }
        public int Add(T item)
        {
            _dbContext.Set<T>().Add(item); // null reference exception
            return _dbContext.SaveChanges(); // will make sql script for all changes 
        }

        public int Delete(T item)
        {
            _dbContext.Set<T>().Remove(item);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _dbContext.Employees.Include(E => E.Department).AsNoTracking().ToList();
            }
            else
            {
                return _dbContext.Set<T>().AsNoTracking().ToList();
            }
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public int Update(T item)
        {
            _dbContext.Set<T>().Update(item);
            return _dbContext.SaveChanges();
        }
    }
}
