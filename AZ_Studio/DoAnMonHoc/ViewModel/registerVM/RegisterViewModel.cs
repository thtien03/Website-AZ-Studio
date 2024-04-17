using System.ComponentModel.DataAnnotations;

namespace DoAnMonHoc.ViewModel.registerVM
{
	public class RegisterViewModel
	{
		[Required]
		public string? Name { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords don't match.")]
		public string? ConfirmPassword { get; set; }
		public string? Address { get; set; }

		public string? Role { get; set; }
	}
}
