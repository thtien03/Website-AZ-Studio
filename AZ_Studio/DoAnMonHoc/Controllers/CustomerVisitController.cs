using DoAnMonHoc.Data;
using DoAnMonHoc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonHoc.Controllers
{
	public class CustomerVisitController : Controller
	{
        private readonly ApplicationDbContext _context;

        public CustomerVisitController(ApplicationDbContext context) {
            this._context = context;
        }
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Index()
		{
			var listMessage = await _context.messageCustomerVisit.ToListAsync();
			return View(listMessage);
		}
		public async Task<IActionResult> ThongTinLienHe(MessageCustomerVisit model) 
		{
            try
			{
				var message = new MessageCustomerVisit
				{
					HoTen = model.HoTen,
					Email = model.Email,
					SoDienThoai = model.SoDienThoai,
					TinNhan = model.TinNhan
				};
				await _context.messageCustomerVisit.AddAsync(message);
				await _context.SaveChangesAsync();
			}
			catch(Exception ex)
			{
				return View(ex.Message);
			}
			return RedirectToAction("Index", "Home");
		}
	}
}
