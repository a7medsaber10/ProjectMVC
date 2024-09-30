using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.DAL.Models;
using ProjectMVC.PL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
            _mapper = mapper;
        }

		public async Task<IActionResult> Index(string email)
		{
            if (string.IsNullOrEmpty(email))
            {
				var users = await _userManager.Users.Select(u => new UserViewModel()
				{
					Id = u.Id,
					FirstName = u.FirstName,
					LastName = u.LastName,
					Email = u.Email,
					PhoneNumber = u.PhoneNumber,
					Roles = _userManager.GetRolesAsync(u).Result
				}).ToListAsync();
				return View(users);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(email);
				if (user != null) 
				{
					var mappedUser = new UserViewModel()
					{
						Id = user.Id,
						FirstName = user.FirstName,
						LastName = user.LastName,
						Email = user.Email,
						PhoneNumber = user.PhoneNumber,
						Roles = _userManager.GetRolesAsync(user).Result
					};
					return View(new List<UserViewModel> { mappedUser });
				}
            }
            return View(Enumerable.Empty<UserViewModel>());
		}

		[HttpGet]
		public async Task<IActionResult> Details(string id, string viewName = "Details")
		{
			if (id is null)
			{
				return BadRequest();
			}
			var User = await _userManager.FindByIdAsync(id);


            if (User is null)
            {
                return NotFound();
            }
			var mappedUser = _mapper.Map<ApplicationUser , UserViewModel>(User);

			return View(viewName, mappedUser);
        }
	}
}
