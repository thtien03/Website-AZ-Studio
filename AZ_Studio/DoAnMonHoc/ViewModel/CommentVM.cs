using DoAnMonHoc.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnMonHoc.ViewModel
{
	public class CommentVM
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Mã comment")]
		public int MaComment { get; set; }

		[Required(ErrorMessage = "*")]
		[Display(Name = "Nội dung comment")]
		[StringLength(500)]
		public string NoiDung { get; set; }

		public int LuotSaoDanhGia { get; set; }
	}
}
