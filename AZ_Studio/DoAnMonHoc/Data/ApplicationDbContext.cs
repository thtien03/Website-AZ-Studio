using DoAnMonHoc.Models;
using DoAnMonHoc.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonHoc.Data
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<TrangThai> TrangThais { get; set; }
		public DbSet<KhachHang> KhachHangs { get; set; }
		public DbSet<DonDatHang> DonDatHangs { get; set; }
		public DbSet<ChiTietDonDatHang> chiTietDonDatHangs { get; set; }
		public DbSet<ThanhToan> thanhToans { get; set; }
		public DbSet<NhaCungCap> nhaCungCaps { get; set; }
		public DbSet<Size> sizes { get; set; }
		public DbSet<VaiTro> vaiTros { get; set; }
		public DbSet<Comment> comments { get; set; }
		public DbSet<HoaDon> hoaDons { get; set; }
		public DbSet<ChiTietHoaDon> chiTietHoaDons { get; set; }
		public DbSet<MessageCustomerVisit> messageCustomerVisit { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.Property(p => p.NgaySanXuat)
				.HasColumnType("date");
			modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => p.UserId);
			modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
			modelBuilder.Entity<IdentityUserToken<string>>().HasKey(p => new { p.UserId, p.LoginProvider, p.Name });
		}

	}
}
