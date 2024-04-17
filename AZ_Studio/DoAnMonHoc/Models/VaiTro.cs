using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnMonHoc.Models
{
	public class VaiTro
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Mã vai trò ")]
		public int MaVaiTro { get; set; }

		[Required(ErrorMessage ="*")]
		[Display(Name = "Tên vai trò")]
		[StringLength(50)]
		public string TenVaiTro { get; set; }

		public ICollection<KhachHang> KhachHangs { get; set; }
	}
}
