using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnMonHoc.Models
{
	public class MessageCustomerVisit
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Mã tin nhắn")]
		public int MaTinNhan { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập họ tên"), StringLength(50, ErrorMessage =
		   "tên người dùng không vượt quá 50 kí tự")]
		[Display(Name = "Mã sản phẩm")]
		public string HoTen { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập email liên hệ"), StringLength(50, ErrorMessage =
		   "email người dùng không hợp lệ")]
		[Display(Name = "Mã sản phẩm")]
		[DataType(DataType.EmailAddress,ErrorMessage ="Email không đúng định dạng")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập số điện thoại"), StringLength(10, ErrorMessage =
		   "số điện thoại dùng không hợp lệ")]
		[Display(Name = "Số điện thoại")]
		public string SoDienThoai { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập tin nhắn"), StringLength(300, ErrorMessage =
		   "tin nhắn không được vượt quá 300 kí tự")]
		[Display(Name = "Tin nhắn")]
		public string TinNhan { get; set; }
	}
}
