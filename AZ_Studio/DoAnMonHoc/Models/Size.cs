using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnMonHoc.Models
{
	public class Size
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name ="Mã size")]
		public int MaSize { get; set; }

		[Required(ErrorMessage ="*")]
		[StringLength(10,ErrorMessage ="Tên size không được vượt quá 10 kí tự")]
		[Display(Name = "Tên size")]
		public string TenSize { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
