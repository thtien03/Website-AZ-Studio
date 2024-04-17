using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnMonHoc.Models
{
	public class DonDatHang
	{
		[Key]
		[StringLength(250,ErrorMessage ="Mã đơn đặt hàng không vượt quá 250 kí tự")]
		[Display(Name ="Mã đơn đặt hàng")]
		public string MaDonDatHang { get; set; }

		[Required(ErrorMessage ="*")]
		[Display(Name = "Địa chỉ giao hàng")]
		[StringLength(250)]
		public string DiaChi { get; set; }

		[Required(ErrorMessage = "*")]
		[Display(Name = "Số điện thoại liên hệ")]
		[StringLength(25)]
		public string SoDienThoai { get; set; }
		[Display(Name = "Ngày đặt hàng")]

		public DateTime NgayDatHang { get; set; }

		[Display(Name = "Ngày giao hàng")]

		public DateTime NgayGiaoHang { get; set; }
		[Display(Name = "Phí vận chuyển")]
		public float PhiVanChuyen { get; set; } = 0;

		[StringLength(250)]
		[Display(Name = "Ghi chú")]
		public string? GhiChu { get; set; }

		[ForeignKey("TrangThai")]
		public int MaTrangThai { get; set; }
		public TrangThai TrangThai { get; set; }

		[ForeignKey("KhachHang")]
		public int MaKhachHang { get; set; }
		public KhachHang KhachHang { get; set; }

		[ForeignKey("ThanhToan")]
		public int MaThanhToan { get; set; }
		public ThanhToan ThanhToan { get; set; }

		public ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }
		public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
	}
}
