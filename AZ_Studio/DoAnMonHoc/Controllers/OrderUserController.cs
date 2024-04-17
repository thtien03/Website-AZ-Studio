using DoAnMonHoc.Data;
using DoAnMonHoc.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonHoc.Controllers
{
    //[Authorize]
    public class OrderUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderUserController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            var chitietHoaDon =  _context.chiTietHoaDons.Include(h => h.HoaDon).AsQueryable();
            var userEmail = User.Identity!.Name;
            if(chitietHoaDon.Count() > 0 && userEmail != null)
            {
                chitietHoaDon = chitietHoaDon.Where(p => p.HoaDon.khachHang.TenDangNhap.ToLower().Trim() == userEmail.ToLower().Trim());
            } 
            int count = chitietHoaDon.Count();
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
                DienThoai = p.HoaDon.khachHang.DienThoai,
                MaSanPham = p.MaSanPham,
                GhiChu = p.HoaDon.GhiChu,
                PhuongThucThanhToan = "Thanh Toán VnPay",
                SoLuong = p.SoLuong,
                anh = p.Product.Hinh ?? "image_invalid.jpg",
                TenSanPham = p.TenSanPham
            });
            var dataResult = await dataVM.ToListAsync();
            return View(dataResult);
        }
    }
}
