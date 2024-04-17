using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnMonHoc.Models
{
	public class KhachHang
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name ="Mã khách hàng")]
		public int MaKhachHang { get; set; }

		[Required(ErrorMessage ="*")]
		[Display(Name ="Tên đăng nhập")]
		[StringLength(250,ErrorMessage ="Tên đăng nhập không được phép vượt quá 250 kí tự")]
		public string TenDangNhap { get; set; }

		[Required(ErrorMessage = "*")]
		[Display(Name = "Tên người dùng")]
		[StringLength(50)]
		public string HoTen { get; set; }
		[Required(ErrorMessage = "*")]

		[StringLength(250)]
		[Display(Name = "Địa chỉ")]
		public string? DiaChi { get; set; }
		[Required(ErrorMessage = "*")]
		[StringLength(25)]
		[Display(Name = "Số điện thoại")]
		public string DienThoai { get; set; }

		[Required(ErrorMessage = "*")]
		[StringLength(50)]
		[EmailAddress(ErrorMessage ="email không đúng định dạng")]
		public string Email { get; set; }

		[ForeignKey("VaiTro")]
		public int MaVaiTro { get; set; }
		public VaiTro VaiTro { get; set; }

		public ICollection<DonDatHang> DonDatHangs { get; set; }
		public ICollection<HoaDon> HoaDons { get; set; }
	}
}
