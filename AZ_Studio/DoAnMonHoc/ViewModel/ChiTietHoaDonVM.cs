using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DoAnMonHoc.Models;

namespace DoAnMonHoc.ViewModel
{
    public class ChiTietHoaDonVM
    {
        [Display(Name ="Mã chi tiết hóa đơn")]
        public int MaChiTietHD { get; set; }
        [Display(Name = "Tên người đặt hàng")]
        public string? HoTen { get; set; }
        [Display(Name = "Ngày đặt hàng")]
        public DateTime NgayDat { get; set; }
        [Display(Name = "Ngày dự kiến giao hàng")]
        public DateTime? NgayGiaoDuKien { get; set; }
        [Display(Name ="Tổng Tiền")]
        public double TongTien {  get; set; }
        [Display(Name ="Số lượng")]
        public int SoLuong {  get; set; }
        [Display(Name = "Tên sản phẩm")]

        public string TenSanPham { get; set; }
        [Display(Name = "Phương thức thanh toán")]
        public string PhuongThucThanhToan { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [Display(Name = "Hình ảnh")]
        public string anh { get; set; }
        [Display(Name = "Ghi chú")]
        public string? GhiChu { get; set; }
        [Display(Name = "Số điện thoại")]
        public string? DienThoai { get; set; }
        [Display(Name = "Phí vận chuyển")]
        public double PhiVanChuyen { get; set; }

        [StringLength(250)]
        [DisplayName("Mã hóa đơn")]
        public string MaHoaDon { get; set; }
        [StringLength(250)]
        [DisplayName("Mã sản phẩm")]
        public string MaSanPham { get; set; }
        
    }
}
