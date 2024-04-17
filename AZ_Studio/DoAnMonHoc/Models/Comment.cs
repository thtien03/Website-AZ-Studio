using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnMonHoc.Models
{
	public class Comment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Mã comment")]
		public int MaComment { get; set; }

		[Required(ErrorMessage ="*")]
		[Display(Name = "Nội dung comment")]
		[StringLength(500)]
		public string NoiDung { get; set; }

		[Required(ErrorMessage = "*")]
		[StringLength(50)]
		[EmailAddress(ErrorMessage ="email không hợp lệ")]
		public string Email { get; set; }
        [Display(Name = "Thời gian bình luận")]

        public DateTime NgayCmt { get; set; }

        public int LuotSaoDanhGia { get; set; }

		[ForeignKey("Product")]
		[StringLength(250)]
		public string MaSanPham { get; set; }
		public Product Product { get; set; }
	}
}
