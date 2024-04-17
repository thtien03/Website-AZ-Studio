using DoAnMonHoc.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DoAnMonHoc.ViewModel
{
	public class ProductVM
	{
		[Key]
		[Required(ErrorMessage = "Mã sản phầm bắt buộc"), StringLength(250, ErrorMessage =
   "mã Sản phẩm không được vượt quá 250 kí tự")]
		[DisplayName("Mã sản phẩm")]
		public string MaSanPham { get; set; }
		[Required(ErrorMessage = "Tên sản phầm bắt buộc"), StringLength(250, ErrorMessage =
		   "mã Sản phẩm không được vượt quá 250 kí tự")]
		[DisplayName("Tên sản phẩm")]
		public string TenSanPham { get; set; }
		[DisplayName("Giá tiền")]
		[Required(ErrorMessage = "Vui lòng nhập giá tiền")]
		[Range(1.000, 1000000.000, ErrorMessage = "Gía sản phẩm phải nằm trong khoảng từ 1.000 đến 1.000.000.000")]
		public double DonGia { get; set; }
		[DisplayName("Hình ảnh")]
		public string? Hinh { get; set; }
		[DisplayName("Hình ảnh chi tiết")]
		public List<ProductImageDetail>? HinhAnhChiTiet { get; set; }
		public int SoLuongBan { get; set; } = 0;
		public int SoLuongXem { get; set; } = 0;
		[DisplayName("Ngày sản xuất")]
		[Required(ErrorMessage = "Chọn ngày sản xuất")]
		[DataType(DataType.Date)]
		public DateTime NgaySanXuat { get; set; }
		[DisplayName("Mô tả")]
		[Required(ErrorMessage = "thêm mô tả")]
		public string? MoTa { get; set; }
		[DisplayName("Số lượng sản phẩm")]
		[Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm")]
		public int SoLuong { get; set; }
		[ForeignKey("ProductCategory")]
		[DisplayName("Mã danh mục")]
		public int MaLoai { get; set; } // Khóa ngoại tham chiếu đến MaLoai trong ProductCategory
		[ForeignKey("NhaCungCap")]
		[DisplayName("Mã nhà cung cấp")]
		[StringLength(50)]
		public string MaNhaCungCap { get; set; }
		[ForeignKey("Size")]
		[DisplayName("Mã size (M,S,XXL,SSL)")]
		public int MaSize { get; set; }
	}
}

