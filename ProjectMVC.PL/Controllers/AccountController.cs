using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectMVC.DAL.Models;
using ProjectMVC.PL.Helpers;
using ProjectMVC.PL.ViewModels;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ProjectMVC.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

        #region Sign Up
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel) 
        { 
            if (ModelState.IsValid) // Server side validations
            {
                var user = new ApplicationUser()
                {
                    UserName = viewModel.Email.Split("@")[0],
                    Email = viewModel.Email,
                    IsAgree = viewModel.IsAgree,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                };
                var result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach (var Error in result.Errors) // Error Handling
                {
                    ModelState.AddModelError(string.Empty, Error.Description);
                }
            }
            return View(viewModel);
        }
        #endregion

        #region Sign In
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel) 
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);
                if (user is not null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(user, viewModel.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }

                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(viewModel);
        }
        #endregion

        #region Sign Out
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);
                if (user is not null)
                {
                    var token = _userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = viewModel.Email , token = token});

                    var email = new Email()
                    {
                        Subject = "Reset Your Password",
                        Body = ResetPasswordUrl,
                        Recepients = viewModel.Email
                    };

                    EmailSettings.SendEmail(email);
                    RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
            }
            return View(viewModel);
        }


        public IActionResult CheckYourInbox()
        {
            return View();
        }
        #endregion

        #region ResetPassword
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPssword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var user = await _userManager.FindByEmailAsync(email);

                var result = await _userManager.ResetPasswordAsync(user, token, viewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(viewModel);
        }
        #endregion
    }
}


