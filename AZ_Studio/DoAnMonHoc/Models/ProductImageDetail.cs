using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
namespace DoAnMonHoc.Models
{
	public class ProductImageDetail
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string? Url { get; set; }
		[ForeignKey("Product")]
		[StringLength(250)]
		public string MaSanPham { get; set; }
		public Product Product { get; set; }
	}
}
