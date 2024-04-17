namespace DoAnMonHoc.ViewModel
{
    public class CardVM
    {
        public string MaSP {  get; set; }
        public string TenSp { get; set; }
        public double DonGia { get; set; }
        public string? Hinh {  get; set; }
        public int soLuong { get; set; }

        public double tongTien => soLuong * DonGia;
    }
}
