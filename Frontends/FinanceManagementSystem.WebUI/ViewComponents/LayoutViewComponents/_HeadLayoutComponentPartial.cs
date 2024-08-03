using Microsoft.AspNetCore.Mvc;

namespace FinanceManagementSystem.WebUI.ViewComponents.LayoutViewComponents
{
    public class _HeadLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
