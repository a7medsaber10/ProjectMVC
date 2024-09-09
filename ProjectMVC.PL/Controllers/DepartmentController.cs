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
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _env;

        //private readonly IDepartmentRepository _repository;

        public DepartmentController(IDepartmentRepository departmentRepository, IWebHostEnvironment env)
        {
            _departmentRepository = departmentRepository;
            _env = env;
            //DepartmentRepository = departmentRepository;
            //_repository = departmentRepository;
        }

        //public IDepartmentRepository DepartmentRepository { get; }
        public IActionResult Index()
        {
            // GetAll
            var department =_departmentRepository.GetAll();
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
                var count = _departmentRepository.Add(department);
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
            var department = _departmentRepository.GetById(id.Value);
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
                _departmentRepository.Update(department);
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
                _departmentRepository.Delete(department);
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
