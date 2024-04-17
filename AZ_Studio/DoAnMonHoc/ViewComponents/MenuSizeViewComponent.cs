using DoAnMonHoc.Data;
using DoAnMonHoc.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonHoc.ViewComponents
{
    public class MenuSizeViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext context;

        public MenuSizeViewComponent(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = context.sizes.Select(type => new MenuSizeVM
            {
                MaSize = type.MaSize,
                TenSize = type.TenSize,
            });
            var result = await data.ToListAsync();
            return View("MenuSizeShop", data);
        }
    }
}
