using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnMonHoc.Models
{
	public class ThanhToan
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Mã thanh toán")]
		public int MaThanhToan { get; set; }

		[Required(ErrorMessage ="*")]
		[StringLength(250)]
		[Display(Name = "Phương thức thanh toán")]
		public string PhuongThucThanhToan { get; set; }

		public ICollection<DonDatHang> DonDatHangs { get; set; }
		public ICollection<HoaDon> HoaDons { get; set; }
	}

}
