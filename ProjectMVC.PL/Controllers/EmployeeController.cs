using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using ProjectMVC.BLL.Interfaces;
using ProjectMVC.BLL.Repositories;
using ProjectMVC.DAL.Models;
using ProjectMVC.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace ProjectMVC.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment env, IMapper mapper/*, IDepartmentRepository departmentRepository*/) 
        {
            _employeeRepository = repository;
            _env = env;
            _mapper = mapper;
            //_departmentRepository = departmentRepository;
        } 

        public IActionResult Index(string searchInput)
        {
            //TempData.Keep();
            if (string.IsNullOrEmpty(searchInput))
            {
                var employees = _employeeRepository.GetAll();
                var mappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedEmployee);

            }
            else
            {
                var employees = _employeeRepository.GetEmployeeByName(searchInput);
                var mappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedEmployee);
            }
            // Extra info
            // Binding through view's Dictionary : transer data from Action to View
            // 1. ViewData
            //ViewData["Message"] = "Hello ViewData";
            // 2. ViewBag
            //ViewBag.Message = "Hello ViewBag";
        }

        public IActionResult Create() 
        {
            //ViewData["Departments"] = _departmentRepository.GetAll();
            //ViewBag.Departments = _departmentRepository.GetAll();
            return View();        
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            // 3. TempData => Action To Action
            if (ModelState.IsValid)
            {
                // manual mapping
                //var mappedEmployee = new Employee()
                //{
                //    Name = employeeVM.Name,
                //    Age = employeeVM.Age,
                //    Address = employeeVM.Address,
                //    Salary = employeeVM.Salary,
                //    Email = employeeVM.Email,
                //    PhoneNumber = employeeVM.PhoneNumber,
                //    IsActive = employeeVM.IsActive,
                //    HireDate = employeeVM.HireDate
                //};

                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                var count = _employeeRepository.Add(mappedEmployee);
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
            return View(employeeVM);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var employee = _employeeRepository.GetById(id.Value);
            var mappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            //ViewBag.Departments = _departmentRepository.GetAll();

            if (employee == null)
            {
                return NotFound(); //404
            }
            return View(viewName, mappedEmployee);
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit"); 
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            try
            {
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _employeeRepository.Update(mappedEmployee);
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
                return View(employeeVM);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {
            try
            {
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _employeeRepository.Delete(mappedEmployee);
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
                return View(employeeVM);
            }
        }
    }
}
