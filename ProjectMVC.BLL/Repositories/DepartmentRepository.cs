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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        // readony --> prevent making instanse from dbContext in any method manually 
        //private readonly AppDbContext _dbContext; // NULL

        public DepartmentRepository(AppDbContext dbContext):base(dbContext) // Ask CLR for creating object from DbContext
        {
            //_dbContext = new AppDbContext(); // With this line the exception is ended
            //_dbContext = dbContext;
        }
    }
}
