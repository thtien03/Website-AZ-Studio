using DoAnMonHoc.Data;
using DoAnMonHoc.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonHoc.ViewComponents
{
    public class MenuShopViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext context;

        public MenuShopViewComponent(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = context.ProductCategories.Select(type => new MenuShopVM
            {
                MaDanhMuc = type.MaLoai,
                TenDanhMuc = type.TenLoai,
                SoLuong = type.Products.Count
            });
            var result = await data.ToListAsync();
            return View("MenuViewShop", data);
        }
    }
}
