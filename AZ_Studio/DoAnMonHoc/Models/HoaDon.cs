namespace DoAnMonHoc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class HoaDon
{
	[Key]
	[Display(Name ="Mã hóa đơn")]
	[StringLength(250)]
	public string MaHoaDon { get; set; }
	public DateTime NgayDat { get; set; }
	public DateTime? NgayGiaoDuKien { get; set; }
	public string? HoTen { get; set; }

	public string PhuongThucThanhToan {  get; set; }
	public string DiaChi { get; set; }
	public string? GhiChu { get; set; }
	public double PhiVanChuyen { get; set; }
	public double tongTien {  get; set; }

	[Display(Name = "Mã khách hàng")]
	[ForeignKey("KhachHang")]
	public int MaKhachHang { get; set; }
	public KhachHang khachHang { get; set; }

	[ForeignKey("ThanhToan")]
	public int MaThanhToan { get; set; }
	public ThanhToan ThanhToan { get; set; }
	[ForeignKey("TrangThai")]
	public int MaTrangThai { get; set; }
	public TrangThai TrangThai { get; set; }

}
