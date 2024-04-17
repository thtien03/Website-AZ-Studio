using DoAnMonHoc.Data;
using DoAnMonHoc.ViewModel;
using DoAnMonHoc.Vnpay;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault")));


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton<IVnPayService, VnPayService>();

builder.Services.AddIdentity<AppUser, IdentityRole>(
	options =>
	{
		options.Password.RequiredUniqueChars = 0; // s? kí t? ??c nh?t là 0 
		options.Password.RequireUppercase = false; // ko yêu c?u có kí t? vi?t hoa
		options.Password.RequiredLength = 8;// ít nh?t 8
		options.Password.RequireLowercase = false; // vi?t th??ng
		options.Password.RequireNonAlphanumeric = false;//ko yc có kí t? ??c bi?t trong mk
	})
	.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); // thi?t l?p Identity sd ENTITY FR ?? l?u tr? d? li?u ng??i dùng
builder.Services.AddScoped<RoleManager<IdentityRole>>();                                                                          //AddDefaultTokenProviders : ?k các cung c?p mã thông báo m?c ??nh (sd trong quá trình xác th?c ng??i dùng)
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminPolicy", policy =>
	{
		policy.RequireRole("admin");
	});
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	 name: "areas",
	 pattern: "{area:exists}/{controller=AdminProduct}/{action=Index}/{id?}"
	 );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
