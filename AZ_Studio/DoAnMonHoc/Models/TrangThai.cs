using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnMonHoc.Models
{
	public class TrangThai
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Mã trạng thái")]
		public int MaTrangThai { get; set; }

		[Required(ErrorMessage ="*")]
		[Display(Name = "Tên trạng thái")]
		[StringLength(100,ErrorMessage ="Tên trạng thái không vượt quá 100 kí tự")]
		public string TenTrangThai { get; set; }

		public ICollection<DonDatHang> DonDatHangs { get; set; }
		public ICollection<HoaDon> HoaDons { get; set; }
	}
}
