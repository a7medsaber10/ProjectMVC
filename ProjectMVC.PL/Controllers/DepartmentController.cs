using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProjectMVC.BLL.Interfaces;
using ProjectMVC.BLL.Repositories;
using ProjectMVC.DAL.Models;
using System;

namespace ProjectMVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        //private readonly IDepartmentRepository _repository;

        public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _env = env;
            //DepartmentRepository = departmentRepository;
            //_repository = departmentRepository;
        }

        //public IDepartmentRepository DepartmentRepository { get; }
        public IActionResult Index()
        {
            // GetAll
            var department = _unitOfWork.DepartmentRepository.GetAll();
            return View(department);
        }
        // Get
        public IActionResult Create()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Department department) 
        {
            if (ModelState.IsValid) 
            {
                _unitOfWork.DepartmentRepository.Add(department); // state added
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if(!id.HasValue)
            {
                return BadRequest(); //400
            }
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department == null)
            {
                return NotFound(); //404
            }
            return View(viewName, department);
        }

        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest(); //400
            //}
            //var department = _departmentRepository.GetById(id.Value);
            //if (department == null)
            //{
            //    return NotFound(); //404
            //}
            //return View(department);

            return Details(id,"Edit"); // this for enhance repeated code
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute] int id , Department department) 
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(department);
            }

            try
            {
                _unitOfWork.DepartmentRepository.Update(department);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex) 
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured while update Department");
                }
                return View(department);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department) 
        {
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured while Delete Department");
                }
                return View(department);
            }
        }
    }
}
