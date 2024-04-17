using DoAnMonHoc.Models;

namespace DoAnMonHoc.ViewModel
{
    public class ChiTietSanPhamVM
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public double DonGia { get; set; }
        public string? Hinh { get; set; }
		public List<ProductImageDetail>? HinhAnhChiTiet { get; set; }

        public List<Comment>? comments { get; set; }
        public string TenDanhMuc {  get; set; }
        public int soLuongXem { get; set; }
        public int soLuongMua { get; set; }
        public string NgaySanXuat {  get; set; }

        public string MoTa {  get; set; }
    }
}
