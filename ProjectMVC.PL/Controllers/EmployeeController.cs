using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProjectMVC.BLL.Interfaces;
using ProjectMVC.BLL.Repositories;
using ProjectMVC.DAL.Models;
using System;
using System.Xml.Schema;

namespace ProjectMVC.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment env) 
        {
            _employeeRepository = repository;
            _env = env;
        } 
        public IActionResult Index()
        {
            TempData.Keep();
            var employees = _employeeRepository.GetAll();
            // Extra info
            // Binding through view's Dictionary : transer data from Action to View
            // 1. ViewData
            ViewData["Message"] = "Hello ViewData";
            // 2. ViewBag
            ViewBag.Message = "Hello ViewBag";

            return View(employees);
        }

        public IActionResult Create() 
        {
            return View();        
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            // 3. TempData => Action To Action
            if (ModelState.IsValid)
            {
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee Created successfully";
                }
                else
                {
                    TempData["Message"] = "An Error Occurred";
                }
                return RedirectToAction(nameof(Index));

            }
            return View(employee);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var employee = _employeeRepository.GetById(id.Value);
            if (employee == null)
            {
                return NotFound(); //404
            }
            return View(viewName, employee);
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit"); 
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            try
            {
                _employeeRepository.Update(employee);
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
                    ModelState.AddModelError(string.Empty, "An Error Occured while update Department");
                }
                return View(employee);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _employeeRepository.Delete(employee);
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
                return View(employee);
            }
        }
    }
}
