using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is Required")]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password Doesn't Match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
