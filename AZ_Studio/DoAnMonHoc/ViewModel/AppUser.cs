using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DoAnMonHoc.ViewModel
{
	public class AppUser : IdentityUser
	{
		[StringLength(100)]
		[MaxLength(100)]
		[Required]
		public string? Name { get; set; }
		public string? Address { get; set; }
		public string? Role { get; set; }
	}
}
