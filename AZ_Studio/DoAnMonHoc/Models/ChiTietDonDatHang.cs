using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnMonHoc.Models
{
	public class ChiTietDonDatHang
	{
		[Key]
		[StringLength(250, ErrorMessage = "Mã chi tiết đơn đặt hàng không vượt quá 250 kí tự")]
		[Display(Name = "Mã chi tiết đơn đặt hàng")]
		public string MaChiTiet { get; set; }
		[Display(Name ="Số lương mua")]
		public int SoLuongSanPhamMua { get; set; }

		[Required(ErrorMessage ="*")]
		[StringLength(250)]
		[Display(Name = "Tên sản phẩm")]
		public string TenSanPham { get; set; }

		[Required(ErrorMessage = "*")]
		[StringLength(250)]
		[Display(Name = "Tên loại sản phẩm")]
		public string TenLoaiSanPham { get; set; }
		[Display(Name = "Đơn giá")]
		public float DonGia { get; set; }
		[Display(Name = "Tổng tiền")]
		public double TongTien { get; set; }
		[ForeignKey("Product")]
		[Display(Name = "Mã sản phẩm")]
		[StringLength(250)]
		public string MaSanPham { get; set; }
		public Product Product { get; set; }

		[ForeignKey("DonDatHang")]
		[Display(Name = "Mã đơn đặt hàng")]
		[StringLength(250)]
		public string MaDonDatHang { get; set; }
		public DonDatHang DonDatHang { get; set; }
	}
}
