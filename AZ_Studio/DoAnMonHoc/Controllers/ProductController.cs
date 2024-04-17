using DoAnMonHoc.Data;
using DoAnMonHoc.Models;
using DoAnMonHoc.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DoAnMonHoc.Controllers
{
	public class ProductController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProductController(ApplicationDbContext context)
		{
			this._context = context;
		}
		[HttpGet]
		public async Task<IActionResult> Index(int? loai)
		{
			IQueryable<Product> products = _context.Products.AsQueryable();
			if (loai.HasValue)
			{
				products = products.Where(p => p.MaLoai == loai.Value);
			}
			IQueryable<SanPhamVM> result = products.Select(p => new SanPhamVM
			{
				MaSanPham = p.MaSanPham,
				TenSanPham = p.TenSanPham,
				DonGia = p.DonGia,
				Hinh = p.Hinh ?? ""
			});
			var data = await result.ToListAsync();
			return View(data);
		}
		public async Task<IActionResult> SearchProduct(string? nameProduct)
		{

			IQueryable<Product> filterProduct = _context.Products.AsQueryable();
			if (nameProduct != null)
			{
				filterProduct = filterProduct.Where(filter => filter.TenSanPham.ToLower().Trim().Contains(nameProduct.ToLower().Trim()));
			}
			var dataFilter = filterProduct.Select(p => new SanPhamVM
			{
				MaSanPham = p.MaSanPham,
				TenSanPham = p.TenSanPham,
				DonGia = p.DonGia,
				Hinh = p.Hinh ?? " "
			});
			var result = await dataFilter.ToListAsync();
			return View(result);
		}
		public string solutionDate(string date)
		{
			//1/1/2003
			string[] split = date.Split('/');
			StringBuilder append = new StringBuilder();
			foreach (string s in split)
			{
				if (s.Length == 1 && s.Length < 4)
				{
					append.Append(0).Append(s);
				}
				else
				{
					append.Append(s);
				}
				if (s.Length != 4) append.Append('/');
			}
			return append.ToString();
		}
		public async Task<IActionResult> Detail(string maSP)
		{
			var filterSanPham = await _context.Products.Include(p => p.ProductCategory).Include(p => p.HinhAnhChiTiet).SingleOrDefaultAsync(p => p.MaSanPham.Trim() == maSP.Trim());
            var listComment = _context.comments.Where(cmt => cmt.MaSanPham.Trim() == maSP.Trim());
            var dataComment = await listComment.ToListAsync();
            if (maSP == null || filterSanPham == null)
			{
				return NotFound();
			}
			var data = new ChiTietSanPhamVM
			{
				MaSanPham = filterSanPham.MaSanPham,
				TenSanPham = filterSanPham.TenSanPham,
				DonGia = filterSanPham.DonGia,
				Hinh = filterSanPham.Hinh ?? " ",
				HinhAnhChiTiet = filterSanPham.HinhAnhChiTiet,
				comments = dataComment,
                soLuongMua = filterSanPham.SoLuongBan,
				soLuongXem = filterSanPham.SoLuongXem,
				TenDanhMuc = filterSanPham.ProductCategory.TenLoai,
				NgaySanXuat = solutionDate(filterSanPham.NgaySanXuat.ToShortDateString()),
				MoTa = filterSanPham.MoTa ?? ""
			};
			return View(data);
		}
		[Authorize]
		public async Task<IActionResult> RatingProduct(CommentVM model, string maSP)
		{
			//model.MaSanPham ??= maSP;
			if (!ModelState.IsValid)
			{
				foreach (var key in ModelState.Keys)
				{
					var errors = ModelState[key].Errors;
					foreach (var error in errors)
					{
						var errorMessage = error.ErrorMessage;
					}
				}
			}
			if (ModelState.IsValid)
			{
				var comment = new Comment
				{
					NoiDung = model.NoiDung ?? " ",
					Email = User.Identity!.Name ?? "Email invalid",
					LuotSaoDanhGia = model.LuotSaoDanhGia,
					NgayCmt = DateTime.Now,
					MaSanPham = maSP
				};
				if(comment != null)
				{
					await _context.comments.AddAsync(comment);
					await _context.SaveChangesAsync();
                    return RedirectToAction("Detail", new { maSP = maSP });
                }
			}
			return NotFound();

        }
		public async Task<IActionResult> GetListComments(string maSP)
		{
			var listComment = _context.comments.Where(cmt => cmt.MaSanPham.Trim() == maSP.Trim());
			var data = await listComment.ToListAsync();
			return PartialView("_DanhSachBinhLuan", data);
		}
	}
}
