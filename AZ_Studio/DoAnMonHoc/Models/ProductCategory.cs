using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnMonHoc.Models
{
	public class ProductCategory
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MaLoai {  get; set; }
		[Required(ErrorMessage = "Tên danh mục là bắt buộc"), StringLength(50, ErrorMessage =
		   "Tên Sản phẩm không được vượt quá 50 kí tự")]
		[DisplayName("Tên Danh Mục")]
		public string TenLoai { get; set; }
		[DisplayName("Mô Tả")]
		public string? MoTa { get; set; }
		[DisplayName("Hình Ảnh")]
		public string? HinhAnh { get; set; }
		public ICollection<Product> Products { get; set; } // collection ko cung cấp pt add,remove,update
		//IEnumerable<> là 1 kdl chỉ cho phép duyệt qua các phần tử , không thể thay đổi cấu trúc hoặc nội dung của collection
	}
}
