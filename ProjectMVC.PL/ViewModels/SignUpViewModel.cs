using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.PL.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name is Required")]
		public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is Required")]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password Doesn't Match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "Required to Agree")]
        public bool IsAgree { get; set; }
    }
}
