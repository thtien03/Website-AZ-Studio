using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnMonHoc.Models
{
	public class ChiTietHoaDon
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaChiTietHD { get; set; }

		public double TongTien {  get; set; }
		public string TenSanPham {  get; set; }
		public int SoLuong {  get; set; }

		[ForeignKey("Product")]
		[StringLength(250)]
		[DisplayName("Mã sản phẩm")]
		public string MaSanPham { get; set; }
		public Product Product { get; set; }
		[ForeignKey("HoaDon")]
		[StringLength(250)]
		[DisplayName("Mã hóa đơn")]
		public string MaHoaDon { get; set; }
		public HoaDon HoaDon { get; set; }
	}
}
