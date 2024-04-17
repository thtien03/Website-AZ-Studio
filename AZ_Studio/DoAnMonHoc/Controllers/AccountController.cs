using DoAnMonHoc.Data;
using DoAnMonHoc.ViewModel;
using DoAnMonHoc.ViewModel.registerVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DoAnMonHoc.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<AppUser> signInManager;
		private readonly UserManager<AppUser> userManager;
		private readonly ApplicationDbContext dbContext;
		private readonly RoleManager<IdentityRole> roleManager;

		public AccountController(SignInManager<AppUser> signInManager, ApplicationDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			this.signInManager = signInManager;
			this.dbContext = dbContext;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (ModelState.IsValid)
			{
				var result = await signInManager.
						PasswordSignInAsync(loginViewModel.Username!, loginViewModel.Password!, loginViewModel.RememberMe, false);
				if (result.Succeeded)
				{
					var user = await userManager.FindByNameAsync(loginViewModel.Username!.ToLower());
					var isInRole = await userManager.IsInRoleAsync(user, "admin");

					if (user != null && isInRole) // Kiểm tra xem người dùng có vai trò admin hay không
					{
						return RedirectToAction("Index", "ProductAdmin");
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				ModelState.AddModelError("", "Invalid login attemp");
				return View(loginViewModel);
			}
			return View(loginViewModel);
		}
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (ModelState.IsValid)
			{
				if (!await roleManager.RoleExistsAsync(registerViewModel.Role))
				{
					// Nếu vai trò chưa tồn tại, bạn có thể tạo mới vai trò ở đây
					var role = new IdentityRole(registerViewModel.Role);
					await roleManager.CreateAsync(role);
				}
				AppUser user = new()
				{
					Name = registerViewModel.Name,
					UserName = registerViewModel.Email,
					Email = registerViewModel.Email,
					Address = registerViewModel.Address,
					Role = registerViewModel.Role
				};
				var result = await userManager.CreateAsync(user, registerViewModel.Password);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, registerViewModel.Role); // Thêm vai trò cho người dùng
					await signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Home");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View();
		}
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
