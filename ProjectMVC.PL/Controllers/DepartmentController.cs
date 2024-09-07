using Microsoft.AspNetCore.Mvc;
using ProjectMVC.BLL.Interfaces;
using ProjectMVC.BLL.Repositories;

namespace ProjectMVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        //private readonly IDepartmentRepository _repository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
            //DepartmentRepository = departmentRepository;
            //_repository = departmentRepository;
        }

        //public IDepartmentRepository DepartmentRepository { get; }

        public IActionResult Index()
        {
            // GetAll
            _departmentRepository.GetAll();
            return View();
        }
    }
}
