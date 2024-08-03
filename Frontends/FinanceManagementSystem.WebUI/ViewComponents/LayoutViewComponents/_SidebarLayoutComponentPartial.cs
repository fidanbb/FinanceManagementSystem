using Microsoft.AspNetCore.Mvc;

namespace FinanceManagementSystem.WebUI.ViewComponents.LayoutViewComponents
{
    public class _SidebarLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
