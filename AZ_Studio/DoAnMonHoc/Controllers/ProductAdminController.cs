using DoAnMonHoc.Data;
using DoAnMonHoc.Models;
using DoAnMonHoc.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonHoc.Controllers
{
	[Authorize(Roles = "admin")]
	public class ProductAdminController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProductAdminController(ApplicationDbContext context)
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
		public async Task<IActionResult> SearchProductAdmin(string? nameProduct)
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
		private async Task setDataCategoriesAsync()
		{
			if (_context != null)
			{
				var productCategories = await _context.ProductCategories.ToListAsync();
				var nccCategories = await _context.nhaCungCaps.ToListAsync();
				var sizeCategories = await _context.sizes.ToListAsync();
				if (productCategories != null && productCategories.Count > 0)
				{
					ViewBag.ProductCategories = new SelectList(productCategories, "MaLoai", "TenLoai");
				}
				else
				{
					ViewBag.ProductCategories = new SelectList(new List<Product>(), "MaLoai", "TenLoai");
				}
				if (nccCategories != null && nccCategories.Count > 0)
				{
					ViewBag.NCCCategories = new SelectList(nccCategories, "MaNhaCungCap", "TenNhaCungCap");
				}
				if (sizeCategories != null && sizeCategories.Count > 0)
				{
					ViewBag.SizeCategories = new SelectList(sizeCategories, "MaSize", "TenSize");

				}
			}
		}
		private async Task<string> SaveImage(IFormFile formFile)
		{
			var savePath = Path.Combine("wwwroot/img/danhmuc", formFile.FileName);
			using (var fileStream = new FileStream(savePath, FileMode.Create))
			{
				await formFile.CopyToAsync(fileStream);
			}
			return  formFile.FileName;
		}
		public async Task<IActionResult> AddNewProduct()
		{
			await setDataCategoriesAsync();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> AddNewProduct(ProductVM product, IFormFile formFile, List<IFormFile> images)
		{
			if (ModelState.IsValid)
			{
				if (formFile != null)
				{
					product.Hinh = await SaveImage(formFile);
				}
				if (images != null)
				{
					product.HinhAnhChiTiet = new List<ProductImageDetail>();
					foreach (var item in images)
					{
						var image = new ProductImageDetail
						{
							MaSanPham = product.MaSanPham,
							Url = await SaveImage(item)
						};
						product.HinhAnhChiTiet.Add(image);
					}
				}
				var productAdd = new Product
				{
					MaSanPham = product.MaSanPham,
					TenSanPham = product.TenSanPham,
					SoLuong = product.SoLuong,
					SoLuongBan = product.SoLuongBan,
					SoLuongXem = product.SoLuongXem,
					NgaySanXuat = product.NgaySanXuat,
					DonGia = product.DonGia,
					Hinh = product.Hinh,
					HinhAnhChiTiet = product.HinhAnhChiTiet,
					MoTa = product.MoTa,
					MaLoai = product.MaLoai,
					MaSize = product.MaSize,
					MaNhaCungCap = product.MaNhaCungCap
				};
				await _context.Products.AddRangeAsync(productAdd);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			await setDataCategoriesAsync();
			return View();
		}
		public async Task<IActionResult> UpdateProduct(string maSP)
		{
			 var product = await _context.Products.Include(p => p.HinhAnhChiTiet).
				SingleOrDefaultAsync(p => p.MaSanPham.ToLower().Trim() == maSP.ToLower().Trim());
			if (product != null)
			{
				var productUpdate = new ProductVM
				{
					MaSanPham = product.MaSanPham,
					TenSanPham = product.TenSanPham,
					SoLuong = product.SoLuong,
					SoLuongBan = product.SoLuongBan,
					SoLuongXem = product.SoLuongXem,
					NgaySanXuat = product.NgaySanXuat,
					DonGia = product.DonGia,
					Hinh = product.Hinh,
					HinhAnhChiTiet = product.HinhAnhChiTiet,
					MoTa = product.MoTa,
					MaLoai = product.MaLoai,
					MaSize = product.MaSize,
					MaNhaCungCap = product.MaNhaCungCap
				};
				await setDataCategoriesAsync();
				return View(productUpdate);
			}
			else
			{
				return NotFound();
			}
		}
		[HttpPost]
		public async Task<IActionResult> UpdateProduct(ProductVM product,IFormFile formFile , List<IFormFile> images)
		{
			if (ModelState.IsValid) { 
				if(formFile != null)
				{
					product.Hinh = await SaveImage(formFile); 
				}
				if (images != null)
				{
					product.HinhAnhChiTiet = new List<ProductImageDetail>();
					foreach (var item in images)
					{
						var image = new ProductImageDetail
						{
							MaSanPham = product.MaSanPham,
							Url = await SaveImage(item)
						};
						product.HinhAnhChiTiet.Add(image);
					}
				}
				var productUpdate = new Product
				{
					MaSanPham = product.MaSanPham,
					TenSanPham = product.TenSanPham,
					SoLuong = product.SoLuong,
					SoLuongBan = product.SoLuongBan,
					SoLuongXem = product.SoLuongXem,
					NgaySanXuat = product.NgaySanXuat,
					DonGia = product.DonGia,
					Hinh = product.Hinh,
					HinhAnhChiTiet = product.HinhAnhChiTiet,
					MoTa = product.MoTa,
					MaLoai = product.MaLoai,
					MaSize = product.MaSize,
					MaNhaCungCap = product.MaNhaCungCap
				};
				 _context.Products.Update(productUpdate);
				await _context.SaveChangesAsync();
				await setDataCategoriesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}
		public async Task<IActionResult> DeleteProduct(string maSP)
		{
			var productDelete = await _context.Products.FindAsync(maSP);
			if (productDelete != null)
			{
				return View(productDelete);
			}
			return NotFound();
		}
		[HttpPost,ActionName("DeleteProduct")]
		public async Task<IActionResult> DeleteConfirmed(string maSP)
		{
			var productDelete = await _context.Products.FindAsync(maSP);
			if (productDelete != null)
			{
				_context.Products.Remove(productDelete);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return NotFound();
		}
        public async Task<IActionResult> DanhSachNguoiDungDatHang()
        {
            var chitietHoaDon = _context.chiTietHoaDons.Include(h => h.HoaDon).AsQueryable();
            var userEmail = User.Identity!.Name;
            var dataVM = chitietHoaDon.Select(p => new ChiTietHoaDonVM
            {
                MaChiTietHD = p.MaChiTietHD,
                NgayDat = p.HoaDon.NgayDat,
                NgayGiaoDuKien = p.HoaDon.NgayGiaoDuKien,
                HoTen = p.HoaDon.HoTen,
                TongTien = p.TongTien,
                PhiVanChuyen = p.HoaDon.PhiVanChuyen,
                DiaChi = p.HoaDon.DiaChi,
                MaHoaDon = p.HoaDon.MaHoaDon,
                MaSanPham = p.MaSanPham,
                DienThoai = p.HoaDon.khachHang.DienThoai,
                GhiChu = p.HoaDon.GhiChu,
                PhuongThucThanhToan = "Thanh Toán VnPay",
                SoLuong = p.SoLuong,
                anh = p.Product.Hinh ?? "image_invalid.jpg",
                TenSanPham = p.TenSanPham
            });
            var dataResult = await dataVM.ToListAsync();
            return View(dataResult);
        }
		public async Task<IActionResult> DoanhThu()
		{
			return View();
		}

	}
}

// sử dụng ViewBag ,ViewData[] để truyền dữ liệu từ Controller sang View
/*
 * ViewData["Person"] = new Person { Name = "John Doe", Age = 30 };

// View
@ViewData["Person"].Name
@ViewData["Person"].Age
 */
