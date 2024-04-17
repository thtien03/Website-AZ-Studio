using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoAnMonHoc.Models
{
	public class NhaCungCap
	{
		[Key]
		[StringLength(50, ErrorMessage = "mã NCC không được vượt quá 50 kí tự")]
		[DisplayName("Mã nhà cung cấp")]
		public string MaNhaCungCap { get; set; }
		[Required(ErrorMessage = "*")]
		[StringLength(250, ErrorMessage = "Tên NCC không được vượt quá 250 kí tự")]
		[DisplayName("Tên nhà cung cấp")]
		public string TenNhaCungCap { get; set; }
		[StringLength(25, ErrorMessage = "Số điện thoại không được vượt quá 25 kí tự")]
		[DisplayName("Số điện thoại")]
		[Required(ErrorMessage ="*")]
		public string SoDienThoai { get; set; }
		[StringLength(200)]
		[DisplayName("Logo nhà cung cấp")]
		public string? Logo { get; set; }

		[StringLength(250)]
		[DisplayName("Địa chỉ")]
		public string? DiaChi { get; set; }

		[Required(ErrorMessage = "*")]
		[DataType(DataType.EmailAddress,ErrorMessage ="Email không đúng định dạng")]
		[StringLength(50)]
		[DisplayName("Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "*")]
		[StringLength(50)]
		[DisplayName("Người liên hệ")]
		public string TenNguoiLienLac { get; set; }

		public ICollection<Product> Products { get; set; }
	}
	// sử dụng navigation properties ICollection để biểu diễn mối quan hệ một-nhiều giữa các bảng
}
