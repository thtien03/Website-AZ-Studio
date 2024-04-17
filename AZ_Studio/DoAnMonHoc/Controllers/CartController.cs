using DoAnMonHoc.Data;
using DoAnMonHoc.Models;
using DoAnMonHoc.Session;
using DoAnMonHoc.ViewModel;
using DoAnMonHoc.Vnpay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DoAnMonHoc.Controllers
{
    public class CartController : Controller
    {
        const string KeyCardSession = "CartSessionItem";
        const string KeyMaHD = "MHD00";
        private readonly ApplicationDbContext _context;
        private readonly IVnPayService _vnPayService;
        private static CheckOutVM model;

        public CartController(ApplicationDbContext context, IVnPayService vnPayService)
        {
            this._context = context;
            this._vnPayService = vnPayService;
        }
        public List<CardVM> listCart
        {
            get
            {
                return HttpContext.Session.Get<List<CardVM>>(KeyCardSession) ?? new List<CardVM>();
            }
        }
        public async Task<IActionResult> Index()
        {
            if (listCart.Count < 0)
            {
                ViewBag.message = "không có sản phẩm nào";
            }
            return View(listCart);
        }
        [Authorize]
        public async Task<IActionResult> ThemVaoGioHang(string? maSP, int soLuongMua = 1)
        {
            var gioHang = listCart;
            // thêm giỏ hàng cũ vào , kiểm tra xem giỏ có cũ đó có sản phẩm mua trước đó mà có trong giỏ hàng chưa , có thì ta cộng thêm số lượng
            var checkItem = gioHang.SingleOrDefault(p => p.MaSP.Trim() == maSP);
            if (maSP == null)
            {
                return NotFound();
            }
            else
            {
                if (checkItem == null)
                {
                    var product = await _context.Products.SingleOrDefaultAsync(p => p.MaSanPham.Trim() == maSP.Trim());
                    if (product != null)
                    {
                        checkItem = new CardVM
                        {
                            MaSP = product.MaSanPham,
                            TenSp = product.TenSanPham,
                            DonGia = product.DonGia,
                            Hinh = product.Hinh ?? "",
                            soLuong = soLuongMua,
                        };
                        gioHang.Add(checkItem);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    checkItem.soLuong += soLuongMua;
                }
                HttpContext.Session.Set(KeyCardSession, gioHang);
                return RedirectToAction("Index");
            }
        }
        [Authorize]
        public async Task<IActionResult> RemoveCart(string loai)
        {
            var giohang = listCart;
            var item = giohang.SingleOrDefault(p => p.MaSP.Trim() == loai.Trim());
            if (item != null)
            {
                giohang.Remove(item);
                await HttpContext.Session.SetAsync(KeyCardSession, giohang);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Checkout()
        {
            if (listCart.Count == 0)
            {
                return Redirect("/");
            }
            return View(listCart);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(CheckOutVM modelCheckout, string payment)
        {

            if (ModelState.IsValid)
            {
                model = modelCheckout;

                if (payment == "2")
                {
                    var vnPay = new OrderInfo
                    {
                        Amount = listCart.Sum(p => p.tongTien + 30000),
                        CreatedDate = DateTime.Now,
                        FullName = model.FirstName + model.LastName,
                        OrderId = new Random().Next(1000, 10000)
                    };
                    return Redirect(_vnPayService.InitialPayment(HttpContext, vnPay));
                }
            }
            return View(listCart);
        }
        [Authorize]
        public async Task<IActionResult> PaymentSuccess()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> PaymentFail()
        {
            return View();
        }
        public async Task<IActionResult> PaymentCallBack()
        {
            var response = _vnPayService.ExecutePayment(Request.Query);

            // 00 :giao dich thanh cong
            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VN Pay: {response?.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }

            var userEmail = User.Identity!.Name;
            var userFind = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            var name = userFind!.Name;
            var thanhtoans = await _context.thanhToans.SingleOrDefaultAsync(p => p.MaThanhToan == 2);
            var trangthais = await _context.TrangThais.SingleOrDefaultAsync(p => p.MaTrangThai == 1);
            var danhSachSanPham = listCart;
            double total = listCart.Sum(p => p.tongTien);
            int countProduct = listCart.Sum(c => c.soLuong);

            if (thanhtoans != null && trangthais != null)
            {
                var ngaydat = DateTime.Now;
                var ngay_giao_du_kien = ngaydat.AddDays(3);
                //Transaction sẽ đảm bảo rằng các thao tác trên cơ sở dữ liệu sẽ được thực thi hoàn toàn hoặc không thực thi một cách an toàn.
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var khachHang = new KhachHang
                        {
                            TenDangNhap = userEmail!,
                            HoTen = name!,
                            DiaChi = model.Address,
                            DienThoai = model.Phone,
                            Email = model.Email,
                            MaVaiTro = 1
                        };

                        await _context.KhachHangs.AddAsync(khachHang);
                        await _context.SaveChangesAsync();

                        var hoaDon = new HoaDon
                        {
                            MaHoaDon = KeyMaHD + (new Random().Next(10000, 999999)).ToString(),
                            HoTen = model.FirstName +" "+ model.LastName,
                            GhiChu = model.Note,
                            NgayDat = ngaydat,
                            NgayGiaoDuKien = ngay_giao_du_kien,
                            PhuongThucThanhToan = "Thanh toán vnPay",
                            PhiVanChuyen = 30000,
                            tongTien = total + 30000,
                            DiaChi = model.Address,
                            MaKhachHang = khachHang.MaKhachHang,
                            khachHang = khachHang,
                            MaThanhToan = thanhtoans.MaThanhToan,
                            MaTrangThai = trangthais.MaTrangThai
                        };
                        await _context.hoaDons.AddAsync(hoaDon);
                        await _context.SaveChangesAsync();


                        foreach (var item in danhSachSanPham)
                        {
                            var chiTietHoaDon = new ChiTietHoaDon
                            {
                                TongTien = item.tongTien,
                                SoLuong = item.soLuong,
                                MaHoaDon = hoaDon.MaHoaDon,
                                TenSanPham = item.TenSp,
                                MaSanPham = item.MaSP  
                            };
                            await _context.chiTietHoaDons.AddAsync(chiTietHoaDon);
                        }
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync(); // Hoàn thành transaction
                        HttpContext.Session.Set(KeyCardSession, new List<CardVM>());

                        return RedirectToAction("PaymentSuccess");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Quay lại trạng thái trước khi thực thi transaction
                        return RedirectToAction("PaymentFail");
                    }
                }
            }
            return RedirectToAction("PaymentSuccess");
        }

    }
}
