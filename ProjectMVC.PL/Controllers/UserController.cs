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

		public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<IActionResult> Index(string email)
		{
            if (string.IsNullOrEmpty(email))
            {
				var users = await _userManager.Users.Select(u => new UserViewModel()
				{
					Id = u.Id,
					FName = u.FirstName,
					LName = u.LastName,
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
						FName = user.FirstName,
						LName = user.LastName,
						Email = user.Email,
						PhoneNumber = user.PhoneNumber,
						Roles = _userManager.GetRolesAsync(user).Result
					};
					return View(new List<UserViewModel> { mappedUser });
				}
            }
            return View(Enumerable.Empty<UserViewModel>());
		}
	}
}
