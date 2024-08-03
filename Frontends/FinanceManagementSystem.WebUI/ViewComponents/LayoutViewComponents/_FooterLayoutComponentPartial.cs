using Microsoft.AspNetCore.Mvc;

namespace FinanceManagementSystem.WebUI.ViewComponents.LayoutViewComponents
{
    public class _FooterLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
